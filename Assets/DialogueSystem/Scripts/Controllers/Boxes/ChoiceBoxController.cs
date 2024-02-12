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
    public class ChoiceBoxController : MonoBehaviour, IDialogueElementController<Branch>, IDialogueElementClickable
    {

        public static event Action SendClickedSignal;

        [Header("Content")]
        [SerializeField] protected string content;
        [Header("Child Objects")]
        [SerializeField] protected GameObject contentObject;
        [Header("Articy Objects")]
        [SerializeField] protected ArticyRef testArticyRef;

        protected RectTransform rectTransform;
        protected IDialogueElementControllerWithContent contentObjectController;

        protected Branch branch;

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

        public Branch Branch { get => branch; private set => branch = value; }
        public ArticyRef TestArticyRef { get => testArticyRef; }

        // Start is called before the first frame update
        void Awake()
        {
            SetReferences();
        }

        void Start()
        {
            SetFields();
        }

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
            var content = ArticyConversions.BranchToText(branch);
            contentObjectController.Content = content;

            ResizeElement();
        }

        public void SetContent(IFlowObject aObject)
        {
            contentObjectController.Content = ArticyConversions.IFlowObjectToText(aObject);

            ResizeElement();
        }


        protected void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
        }

        protected void SetFields()
        {
            contentObjectController.TextColor = Color.gray;
        }


        protected void InvokeSendClickedSignal()
        {
            SendClickedSignal?.Invoke();
        }

        

        public void ResizeElement()
        {
            ResizeSubElements();
            rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);
        }

        protected void ResizeSubElements()
        {
            contentObjectController.ResizeElement();
        }

        public void GreyOutElement(bool isGrey)
        {

        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            contentObjectController.TextColor = Color.yellow;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            contentObjectController.TextColor = Color.gray;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            InvokeSendClickedSignal();
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
        if (GUILayout.Button("Test Articy Ref"))
        {
            Selection.activeGameObject.GetComponent<ChoiceBoxController>().InitializeElement(Selection.activeGameObject.GetComponent<ChoiceBoxController>().TestArticyRef.GetObject());
        }
    }
}
