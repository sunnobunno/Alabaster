using Alabaster.DialogueSystem.Controllers;
using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class DialogueUIController : MonoBehaviour
    {
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
        }

        private void OnDisable()
        {
            DialogueMainTimelineContainer.SendSlideInEndSignal -= ListenSlideInEndSignal;
        }

        private void ListenSlideInEndSignal()
        {

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

