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
        [SerializeField] private float autoScrollBottomThreshhold = 200f;
        [SerializeField] private float scrollSpeed = 3f;

        public float DialogueWidth { get => dialogueWidth; }
        public float ScreenHeight { get => screenHeight; }
        public float AutoScrollBottomThreshhold { get => autoScrollBottomThreshhold; }
        public float ScrollSpeed { get => scrollSpeed; }

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
            //timeLineContainer.ElementList.Last().DestroySelf();
            //Debug.Log("Destroyed choice list container");
            //Debug.Log("Replacing choice with Dialogue Box");
            //timeLineContainer.AddDialogueBox(branch.Target as ArticyObject);
            ////ToggleLastDialogueBoxTitle(false);
            //SendResponseSignal?.Invoke(branch);

            var coListenResponseSignal = CoListenResponseSignal(branch);
            StartCoroutine(coListenResponseSignal);
        }

        private IEnumerator CoListenResponseSignal(Branch branch)
        {
            timeLineContainer.ElementList.Last().DestroySelf();
            timeLineContainer.ElementList[^2]?.GreyOut(true);
            Debug.Log("Destroyed choice list container");
            Debug.Log("Replacing choice with Dialogue Box");
            timeLineContainer.AddChoiceBoxCopy(branch);
            timeLineContainer.LastElement.Child.GetComponent<ChoiceBoxController>().IsActive = false;

            // Wait to send response signal until the last element is resized to avoid vertical layout group errors
            while (timeLineContainer.ElementList.Last().IsResized == false)
            {
                yield return null;
            }

            SendResponseSignal?.Invoke(branch);
        }

        private void ListenContinueSignal(IFlowObject aObject)
        {
            timeLineContainer.ElementList[timeLineContainer.ElementList.Count -2]?.GreyOut(true);
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
            //timeLineContainer.LastElement.Hide();
            //Debug.Log($"{timeLineContainer.LastElement.IsResized} hiiiii");
            //timeLineContainer.LastElement.SlideInElement();

            while (timeLineContainer.LastElement.IsResized == false)
            {
                yield return null;
            }

            timeLineContainer.LastElement.SlideInElement();

            timeLineContainer.AddContinueBox(aObject);
            timeLineContainer.LastElement.Hide();

            timeLineContainer.ResizeContainer();
            timeLineContainer.AutoScrollContainer();

            while (!timeLineContainer.IsElementSlideDone)
            {
                yield return null;
            }
            timeLineContainer.IsElementSlideDone = false;

            timeLineContainer.LastElement.Show();
        }




        public void CreateChoiceEntry(IFlowObject aObject)
        {
            var createChoiceEntry = CoCreateChoiceEntry(aObject);
            StartCoroutine(createChoiceEntry);
        }

        private IEnumerator CoCreateChoiceEntry(IFlowObject aObject)
        {
            timeLineContainer.AddDialogueBox(aObject);
            //Debug.Log($"{timeLineContainer.LastElement.IsResized} hiiiii");
            //timeLineContainer.LastElement.SlideInElement();

            while (timeLineContainer.LastElement.IsResized == false)
            {
                yield return null;
            }

            timeLineContainer.LastElement.SlideInElement();

            timeLineContainer.AddChoiceList(aObject);
            timeLineContainer.LastElement.Hide();

            timeLineContainer.ResizeContainer();
            timeLineContainer.AutoScrollContainer();

            while (!timeLineContainer.IsElementSlideDone)
            {
                yield return null;
            }
            timeLineContainer.IsElementSlideDone = false;

            timeLineContainer.LastElement.Show();
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

