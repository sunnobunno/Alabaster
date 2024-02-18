using Alabaster.DialogueSystem.Controllers;
using Articy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class DialogueUIController : MonoBehaviour
    {
        public static event Action<Branch> SendResponseSignal;
        public static event Action<IFlowObject> SendContinueSignal;

        public static DialogueUIController Instance { get; private set; }

        [SerializeField] private float dialogueWidth;
        [SerializeField] private float screenHeight;

        public float DialogueWidth { get => dialogueWidth; }
        public float ScreenHeight { get => screenHeight; }

        private DialogueMainTimelineContainer timeLineContainer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            SetReferences();
        }

        private void OnEnable()
        {
            DialogueMainTimelineContainer.SendSlideInEndSignal += ListenSlideInEndSignal;
            DialogueMainTimelineContainer.SendResponseSignal += ListenResponseSignal;
            DialogueMainTimelineContainer.SendContinueSignal += ListenContinueSignal;
        }

        private void OnDisable()
        {
            DialogueMainTimelineContainer.SendSlideInEndSignal -= ListenSlideInEndSignal;
            DialogueMainTimelineContainer.SendResponseSignal -= ListenResponseSignal;
            DialogueMainTimelineContainer.SendContinueSignal -= ListenContinueSignal;
        }

        private void ListenSlideInEndSignal()
        {

        }

        private void ListenResponseSignal(Branch branch)
        {
            Debug.Log("Replacing choice with Dialogue Box");
            timeLineContainer.AddDialogueBox(branch.Target as ArticyObject);
            //ToggleLastDialogueBoxTitle(false);
            SendResponseSignal?.Invoke(branch);
        }

        private void ListenContinueSignal(IFlowObject aObject)
        {
            SendContinueSignal?.Invoke(aObject);
        }

        private void SetReferences()
        {
            timeLineContainer = DialogueMainTimelineContainer.Instance;
        }

        private void SetFields()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateDialogueEntry(IFlowObject aObject)
        {
            var createDialogueEntry = CoCreateDialogueEntry(aObject);
            StartCoroutine(createDialogueEntry);
        }

        private IEnumerator CoCreateDialogueEntry(IFlowObject aObject)
        {
            timeLineContainer.AddDialogueBox(aObject);
            timeLineContainer.ElementList.Last().SlideInElement();

            while (!timeLineContainer.IsElementSlideDone)
            {
                yield return null;
            }
            timeLineContainer.IsElementSlideDone = false;

            timeLineContainer.AddContinueBox(aObject);
        }




        public void CreateChoiceEntry(IFlowObject aObject)
        {
            var createChoiceEntry = CoCreateChoiceEntry(aObject);
            StartCoroutine(createChoiceEntry);
        }

        private IEnumerator CoCreateChoiceEntry(IFlowObject aObject)
        {
            timeLineContainer.AddDialogueBox(aObject);
            timeLineContainer.ElementList.Last().SlideInElement();

            while (!timeLineContainer.IsElementSlideDone)
            {
                yield return null;
            }
            timeLineContainer.IsElementSlideDone = false;

            timeLineContainer.AddChoiceList(aObject);
        }



        public void ToggleLastDialogueBoxTitle(bool toggle)
        {
            if (timeLineContainer.ElementList.Last().Child.GetComponent<DialogueBoxController>() != null)
            {
                timeLineContainer.ElementList.Last().Child.GetComponent<DialogueBoxController>().ToggleTitle(false);
            }
        }
    }

    [CustomEditor(typeof(DialogueUIController))]
    public class DialogueUIControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {


            DrawDefaultInspector();

            if (GUILayout.Button("Add Dialogue Box"))
            {
                Selection.activeGameObject.GetComponent<DialogueUIController>().CreateDialogueEntry(DialogueLogicController.Instance.FlowPlayer.CurrentObject.GetObject());
            }
        }
    }
}

