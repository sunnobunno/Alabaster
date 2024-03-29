using Articy.Unity;
using Articy.Unity.Interfaces;
using Alabaster.DialogueSystem.Controllers;
//using Assets.Scripts.Dialogue_System.Controllers.Logic;
using Alabaster.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using Alabaster.DialogueSystem.Utilities;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEngine.Animations.Rigging;

namespace Alabaster.DialogueSystem.Controllers
{
    public class ChoiceListContainerController : DialogueElement, IDialogueElementController<IFlowObject>, IDialogueElementClickable<IFlowObject>
    {

        public static event Action<Branch> SendClickedSignal;

        [Header("Choice Box Prefab")]
        [SerializeField] private GameObject ChoiceBoxPrefab;
        [Header("Articy Object")]
        [SerializeField] private ArticyRef testArticyRef;
        
        private RectTransform rectTransform;
        private List<GameObject> choiceBoxObjects;
        private List<IDialogueElementController<Branch>> choiceBoxControllers;
        private List<Branch> branchList;

        //private bool doubleClickProtection = false;

        public ArticyRef TestArticyRef { get => testArticyRef; }

        private void OnEnable()
        {
            ChoiceBoxController.SendClickedSignal += ListenResponseSignal;
            //Debug.Log("enabled");
        }

        private void OnDisable()
        {
            ChoiceBoxController.SendClickedSignal -= ListenResponseSignal;
        }

        private void ListenResponseSignal(Branch branch)
        {
            //if (doubleClickProtection) { return; }

            //var debug = ArticyConversions.IFlowObjectToText((ArticyObject)branch.Target);

            //Debug.Log($"Response clicked: {debug}");
            //doubleClickProtection = true;
            SendClickedSignal?.Invoke(branch);
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetElementContent(aObject);
        }

        protected override void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        protected override void SetFields()
        {

        }

        private void SetElementContent(IFlowObject aObject)
        {
            var branches = DialogueLogicController.Instance.FlowPlayer.AvailableBranches;
            //Debug.Log(branches.Count);
            //Debug.Log(((ArticyObject)branches[0].Target).Id);
            PopulateResponseChoiceList(branches);
            ResizeElement();
        }

        public override void ResizeElement()
        {
            if (isResized) { return; }


            CallBacks.VoidCallBackWithGameObject callBack = SetResizedTrue;
            ElementResizer.EndOfFrameResizeElementByChildrenSizeDelta(this, callBack);
            
            //DialogueElementUtilities.VoidCallBack callBack = ResizeCallBack;
            //DialogueElementUtilities.CallBackAtEndOfFrame(callBack, this);
        }

        private void PopulateResponseChoiceList(IList<Branch> branches)
        {
            foreach (Branch branch in branches)
            {
                AddResponseChoiceBox(branch);
            }
        }

        public void AddResponseChoiceBox(Branch branch)
        {
            GameObject newResponseChoiceBox = InstantiateResponseChoiceBox();
            ParentDialogueElementToSelf(newResponseChoiceBox);

            newResponseChoiceBox.GetComponent<ChoiceBoxController>().InitializeElement(branch);
        }

        private GameObject InstantiateResponseChoiceBox()
        {
            GameObject newDialogueElement = GameObject.Instantiate(ChoiceBoxPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            //ParentDialogueElementToSelf(newDialogueElement);
            return newDialogueElement;
        }

        private void ParentDialogueElementToSelf(GameObject dialogueElement)
        {
            dialogueElement.transform.SetParent(gameObject.transform, false);
        }

        public override void GreyOut(bool isGrey)
        {
            //throw new NotImplementedException();
        }






        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }

    [CustomEditor(typeof(ChoiceListContainerController))]
    public class ResponseListContainerController02Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Resize Box"))
            {
                Selection.activeGameObject.GetComponent<ChoiceListContainerController>().ResizeElement();
            }
            if (GUILayout.Button("Initialize Element"))
            {
                Selection.activeGameObject.GetComponent<ChoiceListContainerController>().InitializeElement(Selection.activeGameObject.GetComponent<ChoiceListContainerController>().TestArticyRef.GetObject());
            }
        }
    }
}

