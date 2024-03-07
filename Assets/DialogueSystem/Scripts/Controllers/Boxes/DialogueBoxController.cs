using Articy.Unity;
using Articy.Unity.Interfaces;
using Articy.Little_Guy_Syndrome;
using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem.Utilities;
using System;

namespace Alabaster.DialogueSystem.Controllers
{
    public class DialogueBoxController : DialogueElement, IDialogueElementController<IFlowObject>
    {
        public static Action SendInitializedSignal;


        [Header("Content")]
        [SerializeField] private string content;
        [SerializeField] private string title;
        [Header("Child Objects")]
        [SerializeField] private GameObject contentObject;
        [SerializeField] private GameObject titleObject;
        [Header("Articy Object")]
        [SerializeField] private IFlowObject aObject;
        [SerializeField] public ArticyRef TestArticyRef;

        private RectTransform rectTransform;

        private IDialogueElementControllerWithContent contentObjectController;
        private IDialogueElementControllerWithContent titleObjectController;


        private Vector2 contentObjectInitialLocalPosition;

        public TextBoxLargeImageAssets TextBoxImageAssets {
            get => ((TextBoxLargeController)contentObjectController).TextBoxImageAssets;
            set => ((TextBoxLargeController)contentObjectController).TextBoxImageAssets = value;
        }

        public string Content
        {
            get => contentObjectController.Content;
            set => contentObjectController.Content = value;
        }

        protected override void SetReferences()
        {
            rectTransform = GetComponent<RectTransform>();

            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
            titleObjectController = titleObject.GetComponent<IDialogueElementControllerWithContent>();
        }

        protected override void SetFields()
        {
            contentObjectInitialLocalPosition = contentObject.transform.localPosition;
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetElementContent(aObject);
            SendInitializedSignal?.Invoke();
        }

        public void SetElementContent(IFlowObject aObject)
        {
            //var dialogueSpeaker = aObject as IObjectWithSpeaker;
            //var dialogueEntity = dialogueSpeaker.Speaker as Entity;

            this.aObject = aObject;

            content = ArticyConversions.IFlowObjectToText(aObject);
            //Debug.Log(content);
            title = ArticyConversions.IFlowObjectToTitle(aObject);
            //Debug.Log(title);

            //Debug.Log(content);

            SetContent(content);
            SetTitle(title);

            ResizeElement();
        }

        public void SetContent(string content)
        {
            contentObjectController.InitializeElement(content);
        }

        public void SetTitle(string title)
        {
            titleObjectController.InitializeElement(title);
        }

        

        public override void ResizeElement()
        {
            //if (isResized) return;
            
            ResizeSubElements();
            CallBacks.VoidCallBackWithGameObject callBack = SetResizedTrue;
            ElementResizer.EndOfFrameResizeElementByChildrenSizeDelta(this, callBack);

            //LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            //ElementResizer.ResizeElementByChildrenSizeDelta(gameObject);
            //SetResizedTrue(gameObject);
        }

        private void ResizeSubElements()
        {
            titleObjectController.ResizeElement();
            contentObjectController.ResizeElement();
        }

        private IEnumerator CoResizeElement()
        {
            ResizeSubElements();

            DialogueElement titleController = (DialogueElement)titleObjectController;
            DialogueElement contentController = (DialogueElement)contentObjectController;

            while (titleController.IsResized == false && contentController == false)
            {
                yield return null;
            }

            CallBacks.VoidCallBackWithGameObject callBack = SetResizedTrue;
            ElementResizer.EndOfFrameResizeElementByChildrenSizeDelta(this, callBack);

            Debug.Log($"{gameObject.name}: resized");
        }

        public override void GreyOut(bool isGrey)
        {
            titleObjectController.GreyOut(isGrey);
            contentObjectController.GreyOut(isGrey);
        }

        public void ToggleTitle(bool toggle)
        {
            titleObject.SetActive(toggle);

            if (!toggle)
            {
                contentObject.transform.localPosition = new Vector2(contentObject.transform.localPosition.x, 0f);
            }
            else
            {
                contentObject.transform.localPosition = contentObjectInitialLocalPosition;
            }

            ResizeElement();
        }
    }
}
