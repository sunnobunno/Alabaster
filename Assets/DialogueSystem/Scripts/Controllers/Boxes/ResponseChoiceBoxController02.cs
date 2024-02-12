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
    public class ResponseChoiceBoxController02 : MonoBehaviour, IDialogueElementController<Branch>, IDialogueElementClickable
    {

        public static event Action SendClickedSignal;

        [Header("Content")]
        [SerializeField] protected string content;

        [Header("Child Objects")]
        [SerializeField] protected GameObject contentObject;

        protected RectTransform rectTransform;
        protected RectTransform contentRectTransform;
        protected TextMeshProUGUI contentTextMesh;
        private IDialogueElementControllerWithContent contentObjectController;

        protected Branch branch;
        protected bool componentReferencesSet = false;

        // Start is called before the first frame update
        private void Awake()
        {
            SetElementComponentReferences();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Initialize Element

        public void InitializeElement(Branch branch)
        {
            SetElementContent(branch);
        }

        #endregion


        public void SetElementContent(Branch branch)
        {
            //Debug.Log(branch.BranchId);

            this.branch = branch;

            var branchTarget = branch.Target;

            Debug.Log(ArticyConversions.IFlowObjectToText((ArticyObject)branchTarget));

            var objectWithText = branchTarget as IObjectWithText;
            var text = objectWithText.Text;

            contentTextMesh.text = text;
            content = text;

            ResizeElement();
        }


        protected void SetElementComponentReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            contentRectTransform = contentObject.GetComponent<RectTransform>();
            contentTextMesh = contentObject.GetComponentInChildren<TextMeshProUGUI>();

            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();

            componentReferencesSet = true;
        }

        protected void SetElementSizeDelta()
        {
            contentObjectController.ResizeElement();
            rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);
        }





        protected void InvokeSendClickedSignal()
        {
            SendClickedSignal?.Invoke();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            contentTextMesh.color = Color.yellow;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            contentTextMesh.color = Color.gray;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            InvokeSendClickedSignal();
        }

        public void ResizeElement()
        {
            SetElementSizeDelta();
        }

        public void GreyOutElement(bool isGrey)
        {

        }
    }
}


[CustomEditor(typeof(ResponseChoiceBoxController02))]
public class ResponseChoiceBoxController02Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Box"))
        {
            Selection.activeGameObject.GetComponent<ResponseChoiceBoxController02>().ResizeElement();
        }
    }
}
