﻿using Alabaster.DialogueSystem.Utilities;
using Articy.Unity;
using Alabaster.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Articy.Little_Guy_Syndrome;

namespace Alabaster.DialogueSystem
{
    public class DialogueLogicController : MonoBehaviour, IArticyFlowPlayerCallbacks
    {
        public static DialogueLogicController Instance { get; private set; }

        public static event Action DialogueStart;
        public static event Action DialogueEnd;
        
        [SerializeField] private ArticyFlowPlayer flowPlayer;
        [SerializeField] private ArticyRef testArticyRef;

        private DialogueUIController dialogueUIController;
        private bool isPaused;

        public bool IsPaused { get { return isPaused; } }

        public ArticyFlowPlayer FlowPlayer
        {
            get { return flowPlayer; }
        }

        public ArticyRef TestArticyRef { get { return testArticyRef; } }

        public void StartDialogue(ArticyRef articyRef)
        {
            var articyObject = articyRef.GetObject();

            Debug.Log(articyObject.Id);

            flowPlayer.StartOn = articyObject;
            isPaused = false;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            SetReferences();
        }

        private void Start()
        {
            SetFields();
        }

        private void OnEnable()
        {
            DialogueUIController.SendContinueSignal += ListenContinueSignal;
        }

        private void OnDisable()
        {
            DialogueUIController.SendContinueSignal -= ListenContinueSignal;
        }

        private void ListenContinueSignal(IFlowObject aObject)
        {
            isPaused = false;
            flowPlayer.Play();
        }

        private void SetReferences()
        {
            dialogueUIController = DialogueUIController.Instance;
            flowPlayer = GetComponent<ArticyFlowPlayer>();
        }
        private void SetFields()
        {
            isPaused = true;
        }




        public void OnFlowPlayerPaused(IFlowObject aObject)
        {
            Debug.Log(ArticyConversions.IFlowObjectToText(aObject));

            //DialogueUIController.Instance.CreateDialogueEntry(aObject);

            if (IsNextObjectChoice())
            {
                Debug.Log("Choice");
                DialogueUIController.Instance.CreateChoiceEntry(aObject);
            }
            else
            {
                Debug.Log("Not Choice");
                DialogueUIController.Instance.CreateDialogueEntry(aObject);
            }

            isPaused = true;
        }

        public void OnBranchesUpdated(IList<Branch> aBranches)
        {
            
        }

        private bool IsNextObjectChoice()
        {
            var nextObjectIsChoice = false;

            var aObject = flowPlayer.CurrentObject.GetObject();
            var firstBranch = GetFirstBranch(aObject);
            var firstBranchProperties = GetDialogueChoiceProperties(firstBranch);
            if (firstBranchProperties != null) nextObjectIsChoice = true;

            return nextObjectIsChoice;
        }

        private Dialogue_Choice_Properties GetDialogueChoiceProperties(Branch aBranch)
        {
            var branchObj = aBranch.Target as ArticyObject;
            var branchObjRef = (ArticyRef)branchObj;
            var branchObjProperties = branchObjRef.GetObject<Dialogue_Choice_Properties>();

            return branchObjProperties;
        }

        private Dialogue_Choice_Properties GetDialogueChoiceProperties(ArticyObject aObject)
        {
            var aObjectRef = (ArticyRef)aObject;
            var aObjectProperties = aObjectRef.GetObject<Dialogue_Choice_Properties>();

            return aObjectProperties;
        }

        private Branch GetFirstBranch()
        {
            var currentObject = flowPlayer.CurrentObject.GetObject();
            var firstBranch = GetFirstBranch(currentObject);

            return firstBranch;
        }

        private Branch GetFirstBranch(ArticyObject aObject)
        {
            var nextBranches = ArticyFlowPlayer.GetBranchesOfNode(aObject, flowPlayer);
            var firstBranch = nextBranches[0];

            return firstBranch;
        }
    }

    [CustomEditor(typeof(DialogueLogicController))]
    public class DialogueLogicControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Start Dialogue"))
            {
                Selection.activeGameObject.GetComponent<DialogueLogicController>().StartDialogue(DialogueLogicController.Instance.TestArticyRef);
            }
        }
    }

}
