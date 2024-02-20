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

namespace Alabaster.DialogueSystem.Controllers
{
    public class DialogueBoxController : MonoBehaviour, IDialogueElementController<IFlowObject>
    {

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

        private void Awake()
        {
            SetReferences();
            Debug.Log($"{gameObject.name}: Element Awake");
        }

        void Start()
        {
            SetFields();
        }

        private void SetReferences()
        {
            rectTransform = GetComponent<RectTransform>();

            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
            titleObjectController = titleObject.GetComponent<IDialogueElementControllerWithContent>();
        }

        private void SetFields()
        {
            contentObjectInitialLocalPosition = contentObject.transform.localPosition;
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetElementContent(aObject);
        }

        public void SetElementContent(IFlowObject aObject)
        {
            //var dialogueSpeaker = aObject as IObjectWithSpeaker;
            //var dialogueEntity = dialogueSpeaker.Speaker as Entity;

            this.aObject = aObject;

            content = ArticyConversions.IFlowObjectToText(aObject);
            title = ArticyConversions.IFlowObjectToTitle(aObject);

            //Debug.Log(content);

            SetContent(content);
            InitializeTitleObject(title);

            ResizeElement();
        }

        public void SetContent(string content)
        {
            contentObjectController.InitializeElement(content);
        }

        public void InitializeTitleObject(string title)
        {
            titleObjectController.InitializeElement(title);
        }

        private void ResizeSubElements()
        {
            titleObjectController.ResizeElement();
            contentObjectController.ResizeElement();
        }

        public void ResizeElement()
        {
            ResizeSubElements();
            ElementResizer.EndOfFrameResizeElementByChildrenSizeDelta(this);
        }

        public void GreyOutElement(bool isGrey)
        {
            titleObjectController.GreyOutElement(isGrey);
            contentObjectController.GreyOutElement(isGrey);
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


[CustomEditor(typeof(DialogueBoxController))]
public class DialogueBoxControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Box"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().ResizeElement();
        }
        if (GUILayout.Button("Initialize Box"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().InitializeElement(Selection.activeGameObject.GetComponent<DialogueBoxController>().TestArticyRef.GetObject());
        }
        if (GUILayout.Button("Toggle Title Off"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().ToggleTitle(false);
        }
        if (GUILayout.Button("Toggle Title On"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().ToggleTitle(true);
        }
        if (GUILayout.Button("Grey Out Element"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().GreyOutElement(true);
        }
        if (GUILayout.Button("White Out Element"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().GreyOutElement(false);
        }
    }
}
