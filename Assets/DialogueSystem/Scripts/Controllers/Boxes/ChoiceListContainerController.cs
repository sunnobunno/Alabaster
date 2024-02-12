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
//using UnityEngine.Animations.Rigging;

namespace Alabaster.DialogueSystem.Controllers
{
    public class ChoiceListContainerController : MonoBehaviour, IDialogueElementController<IFlowObject>
    {

        public static event Action SendClickedSignal;

        [Header("Choice Box Prefab")]
        [SerializeField] private GameObject ChoiceBoxPrefab;
        [Header("Articy Object")]
        [SerializeField] private ArticyRef testArticyRef;
        
        private RectTransform rectTransform;
        private List<GameObject> choiceBoxObjects;
        private List<IDialogueElementController<Branch>> choiceBoxControllers;
        private List<Branch> branchList;

        public ArticyRef TestArticyRef { get => testArticyRef; }

        void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            ChoiceBoxController.SendClickedSignal += ListenResponseSignal;
        }

        private void OnDisable()
        {
            ChoiceBoxController.SendClickedSignal -= ListenResponseSignal;
        }

        private void ListenResponseSignal()
        {
            SendClickedSignal?.Invoke();
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetElementContent(aObject);
            SetElementProperties();
        }

        private void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        private void SetFields()
        {

        }

        private void SetElementProperties()
        {
            //Debug.Log($"Dialogue Width: {DialogueUIController02.Instance.DialogueWidth}");
            //rectTransform.sizeDelta = new Vector2(DialogueUIController02.Instance.DialogueWidth, rectTransform.sizeDelta.y);
        }

        private void SetElementContent(IFlowObject aObject)
        {
            var branches = DialogueLogicController.Instance.FlowPlayer.AvailableBranches;
            Debug.Log(branches.Count);
            Debug.Log(((ArticyObject)branches[0].Target).Id);
            PopulateResponseChoiceList(branches);
        }

        public void ResizeElement()
        {
            SetElementProperties();
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

        public void GreyOutElement(bool isGrey)
        {
            //throw new NotImplementedException();
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

