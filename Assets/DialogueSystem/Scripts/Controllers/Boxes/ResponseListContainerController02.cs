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
    public class ResponseListContainerController02 : MonoBehaviour, IDialogueElementController<IFlowObject>
    {

        public static event Action SendClickedSignal;

        [SerializeField] private GameObject ResponseChoiceBoxPrefab;

        [HideInInspector] public RectTransform rectTransform;

        [HideInInspector] public List<GameObject> responseBoxList;
        [HideInInspector] public List<Branch> branchList;

        private List<string> testResponses;

        void Awake()
        {
            SetElementComponentReferences();
        }

        private void OnEnable()
        {
            ResponseChoiceBoxController02.SendClickedSignal += ListenResponseSignal;
        }

        private void OnDisable()
        {
            ResponseChoiceBoxController02.SendClickedSignal -= ListenResponseSignal;
        }

        private void ListenResponseSignal()
        {
            SendClickedSignal?.Invoke();
        }

        public void InitializeElement(IFlowObject aObject)
        {
            //testResponses = new List<string>
            //{
            //    "Hiiiiii.",
            //    "This is a test.",
            //    "Wow!!!"
            //};

            SetElementContent(aObject);
            SetElementProperties();
        }

        private void SetElementComponentReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        private void SetElementProperties()
        {
            //Debug.Log($"Dialogue Width: {DialogueUIController02.Instance.DialogueWidth}");
            //rectTransform.sizeDelta = new Vector2(DialogueUIController02.Instance.DialogueWidth, rectTransform.sizeDelta.y);
        }

        private void SetElementContent(IFlowObject aObject)
        {
            //var branches = DialogueLogicController02.Instance.FlowPlayer.AvailableBranches;
            //Debug.Log(branches.Count);
            //Debug.Log(((ArticyObject)branches[0].Target).Id);
            //PopulateResponseChoiceList(branches);
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

            newResponseChoiceBox.GetComponent<ResponseChoiceBoxController02>().InitializeElement(branch);
        }

        private GameObject InstantiateResponseChoiceBox()
        {
            GameObject newDialogueElement = GameObject.Instantiate(ResponseChoiceBoxPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
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

    [CustomEditor(typeof(ResponseListContainerController02))]
    public class ResponseListContainerController02Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Resize Box"))
            {
                Selection.activeGameObject.GetComponent<ResponseListContainerController02>().ResizeElement();
            }
        }
    }
}

