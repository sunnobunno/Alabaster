using Articy.Unity.Interfaces;
using Articy.Unity;
using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using System;
using Alabaster.DialogueSystem;
using Alabaster.DialogueSystem.Utilities;

namespace Alabaster.DialogueSystem.Controllers
{
    public class ChoiceBoxController : DialogueElement, IDialogueElementController<Branch>, IDialogueElementClickable<Branch>
    {

        public static event Action<Branch> SendClickedSignal;

        [Header("Content")]
        [SerializeField] protected string content;
        [Header("Child Objects")]
        [SerializeField] protected GameObject contentObject;
        [Header("Articy Objects")]
        [SerializeField] protected ArticyRef testArticyRef;

        protected RectTransform rectTransform;
        protected IDialogueElementControllerWithContent contentObjectController;

        protected Branch branch;
        protected bool isActive = true;

        public TextBoxLargeImageAssets TextBoxImageAssets
        {
            get => ((TextBoxLargeController)contentObjectController).TextBoxImageAssets;
            set => ((TextBoxLargeController)contentObjectController).TextBoxImageAssets = value;
        }

        public string Content
        {
            get => contentObjectController.Content;
            set => contentObjectController.Content = value;
        }

        public bool IsActive { get => isActive;
            set
            {
                if (value == true) { isActive = value; }
                if (value == false)
                {
                    isActive = value;
                    SetInactive();
                }
            }
        }

        public Branch Branch { get => branch; private set => branch = value; }
        public ArticyRef TestArticyRef { get => testArticyRef; }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitializeElement(Branch branch)
        {
            SetContent(branch);
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetContent(aObject);
        }

        public void SetContent(Branch branch)
        {
            this.branch = branch;
            
            var content = ArticyConversions.BranchToText(branch);
            contentObjectController.Content = content;

            ResizeElement();
        }

        public void SetContent(IFlowObject aObject)
        {
            contentObjectController.Content = ArticyConversions.IFlowObjectToText(aObject);

            ResizeElement();
        }


        protected override void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
        }

        protected override void SetFields()
        {
            contentObjectController.TextColor = Color.gray;
        }


        //protected void InvokeSendClickedSignal()
        //{
        //    SendClickedSignal?.Invoke(branch);
        //}

        

        public void ResizeElement()
        {
            ResizeSubElements();
            rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);
        }

        protected void ResizeSubElements()
        {
            contentObjectController.ResizeElement();
        }

        public override void GreyOut(bool isGrey)
        {

        }

        private void SetInactive()
        {
            contentObjectController.TextColor = Color.white;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!isActive) return;
            
            contentObjectController.TextColor = Color.yellow;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (!isActive) return;

            contentObjectController.TextColor = Color.gray;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!isActive) return;

            //Debug.Log("choice clicked");
            SendClickedSignal?.Invoke(branch);
            //DestroySelf();
        }

        public void DestroySelf()
        {
            if (transform.parent.parent.GetComponent<BoxContainer>() != null)
            {
                transform.parent.parent.GetComponent<BoxContainer>().DestroySelf();
            }
            else
            {
                Debug.Log($"{gameObject.name}: no BoxContainer parent.");
                Destroy(gameObject);
            }
        }
    }
}


[CustomEditor(typeof(ChoiceBoxController))]
public class ResponseChoiceBoxController02Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Box"))
        {
            Selection.activeGameObject.GetComponent<ChoiceBoxController>().ResizeElement();
        }
        //if (GUILayout.Button("Test Articy Ref"))
        //{
        //    Selection.activeGameObject.GetComponent<ChoiceBoxController>().InitializeElement(Selection.activeGameObject.GetComponent<ChoiceBoxController>().TestArticyRef.GetObject());
        //}
    }
}
