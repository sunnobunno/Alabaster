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
        public static event Action<IFlowObject> SendHoverSignal;
        public static event Action SendExitSignal;

        [Header("Content")]
        [SerializeField] protected string content;
        [Header("Child Objects")]
        [SerializeField] protected GameObject contentObject;
        [SerializeField] private Animator diceAnimator;
        [SerializeField] private GameObject diceContainer;
        [Header("Testing")]
        [SerializeField] protected ArticyRef testArticyRef;
        [SerializeField] private bool debug;


        protected RectTransform rectTransform;
        protected IDialogueElementControllerWithContent contentObjectController;
        

        protected Branch branch;
        private IFlowObject aObject;
        protected bool isActive = true;
        private bool isSkillCheck = false;

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
            aObject = branch.Target;

            isSkillCheck = ArticyConversions.GetIsSkillCheck(aObject);

            var content = ArticyConversions.BranchToText(branch);
            contentObjectController.Content = content;

            if (!isSkillCheck) diceContainer.gameObject.SetActive(false);

            //Debug.Log(diceContainer.gameObject.activeSelf);

            ResizeElement();
        }

        public void SetContent(IFlowObject aObject)
        {
            this.aObject = aObject;
            isSkillCheck = ArticyConversions.GetIsSkillCheck(aObject);

            contentObjectController.Content = ArticyConversions.IFlowObjectToText(aObject);

            if (!isSkillCheck) diceContainer.gameObject.SetActive(false);

            Debug.Log(diceContainer.gameObject.activeSelf);

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


        public override void ResizeElement()
        {
            //if (isResized)
            //{
            //    return;
            //}
            
            ResizeSubElements();
            ElementResizer.ResizeElementByChildrenSizeDelta(gameObject);
            SetResizedTrue(gameObject);
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

            if (isSkillCheck)
            {
                diceAnimator.SetBool("Hover", true);
                SendHoverSignal?.Invoke(aObject);
            }

            

            contentObjectController.TextColor = Color.yellow;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (!isActive) return;

            if (isSkillCheck)
            {
                diceAnimator.SetBool("Hover", false);
                SendExitSignal?.Invoke();
            }

            contentObjectController.TextColor = Color.gray;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!isActive) return;

            if (isSkillCheck)
            {
                SkillCheckInfoController.Instance.RollDice();
            }

            if (debug) return;
            SendClickedSignal?.Invoke(branch);
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

        var selection = Selection.activeGameObject.GetComponent<ChoiceBoxController>();

        if (GUILayout.Button("Resize Box"))
        {
            selection.ResizeElement();
        }
        if (GUILayout.Button("Initialize"))
        {
            selection.InitializeElement(selection.TestArticyRef.GetObject());
        }
    }
}
