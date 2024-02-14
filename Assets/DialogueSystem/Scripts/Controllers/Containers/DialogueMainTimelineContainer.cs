using Articy.Unity;
using Alabaster.DialogueSystem.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem.Controllers
{
    public class DialogueMainTimelineContainer : MonoBehaviour
    {

        public static event Action SendResponseSignal;
        public static event Action SendContinueSignal;

        public static DialogueMainTimelineContainer Instance { get; private set; }

        [SerializeField] private GameObject dialogueElementContainerPrefab;
        [SerializeField] private GameObject dialougeBoxPrefab;
        [SerializeField] private GameObject continueBoxPrefab;
        [SerializeField] private GameObject responseListContainerPrefab;
        [SerializeField] private ArticyRef testArticyRef;

        #region Properties
        public GameObject DialogueElementContainerPrefab { get => dialogueElementContainerPrefab; }
        public GameObject DialogueBoxPrefab { get => dialougeBoxPrefab; }
        public GameObject ContinueBoxPrefab { get => continueBoxPrefab; }
        public GameObject ResponseListContainerPrefab { get => responseListContainerPrefab; }
        public ArticyRef TestArticyRef { get => testArticyRef; }
        public float ElementCount { get => dialogueElementList.Count; }
        public List<GameObject> ElementList { get => dialogueElementList; }
        #endregion



        private List<GameObject> dialogueElementList = new();

        private bool slideInEndSignalRecieved = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            ContinueBoxController.SendClickedSignal += ListenContinueSignal;
            ChoiceListContainerController.SendClickedSignal += ListenResponseSignal;
            BoxContainer.SendSlideInEndSignal += ListenSlideInEndSignal;
        }

        private void OnDisable()
        {
            ContinueBoxController.SendClickedSignal -= ListenContinueSignal;
            ChoiceListContainerController.SendClickedSignal -= ListenResponseSignal;
            BoxContainer.SendSlideInEndSignal -= ListenSlideInEndSignal;
        }

        private void ListenResponseSignal(Branch branch)
        {
            Debug.Log("Choice Clicked");
            SendResponseSignal?.Invoke();
        }

        private void ListenContinueSignal(IFlowObject aObject)
        {
            Debug.Log("Continue Clicked");
            SendContinueSignal?.Invoke();
        }

        private void ListenSlideInEndSignal()
        {
            Debug.Log("SlideInEndSignal Recieved");
            slideInEndSignalRecieved = true;
        }

        public void InitializeElement()
        {

        }

        //public void CreateDialogueEntry()
        //{
        //    IEnumerator createDialogueEntryCoroutine = CreateDialogueEntryCoroutine();
        //    StartCoroutine(createDialogueEntryCoroutine);
        //}

        //private IEnumerator CreateDialogueEntryCoroutine()
        //{
        //    Debug.Log("CreateDialogueEntryCoroutine started");

        //    //AddDialogueBox(true);
        //    AddDialogueElement(DialogueBoxPrefab, testArticyRef.GetObject(), true);

        //    while (!slideInEndSignalRecieved)
        //    {
        //        yield return null;
        //    }

        //    slideInEndSignalRecieved = false;
        //    AddDialogueElement(ContinueBoxPrefab, testArticyRef.GetObject(), false);
        //}

        public void AddDialogueBox(IFlowObject aObject)
        {
            AddDialogueElement(DialogueBoxPrefab, aObject);
        }

        public void AddContinueBox(IFlowObject aObject)
        {
            AddDialogueElement(ContinueBoxPrefab, aObject);
        }

        public void AddChoiceList(IFlowObject aObject)
        {
            AddDialogueElement(ResponseListContainerPrefab, aObject);
        }

        // The goal of this class is to create an interface to perform all the requirements
        // You should be able to add dialogue elements
        // Control the display of their title cards, grey them out, 

        public void AddDialogueElement(GameObject dialogueElementPrefab, IFlowObject aObject)
        {
            Debug.Log($"----- NEW {dialogueElementPrefab.name} -----");

            GameObject newBoxContainer = InstantiateDialogueElementContainer();
            GameObject newDialogueElement = InstantiateDialogueElement(dialogueElementPrefab);
            ParentDialogueElementToSelf(newBoxContainer);
            ParentDialogueElementToContainer(newDialogueElement, newBoxContainer);
            AddDialogueElementToElementList(newBoxContainer);

            newDialogueElement.GetComponent<IDialogueElementController<IFlowObject>>().InitializeElement(aObject);
            newBoxContainer.GetComponent<BoxContainer>().InitializeElement();

            //DialogueElementController controller = dialogueElementPrefab.GetComponent<DialogueElementController>();

            //if (slideInElement)
            //{
            //    newDialogueElementContainer.GetComponent<BoxContainer>().SlideInElement();
            //}
        }


        /*
        public void AddDialogueBox(bool slideInElement)
        {
            Debug.Log("----- NEW DIALOGUE BOX -----");

            GameObject newDialogueElementContainer = InstantiateDialogueElementContainer();
            GameObject newDialogueElement = InstantiateDialogueBox();
            ParentDialogueElementToSelf(newDialogueElementContainer);
            ParentDialogueElementToContainer(newDialogueElement, newDialogueElementContainer);
            AddDialogueElementToElementList(newDialogueElementContainer);

            newDialogueElement.GetComponent<DialogueBoxController02>().InitializeElement("Test", "Testing 1 2 3.");
            newDialogueElementContainer.GetComponent<DialogueElementContainerController>().InitializeElement();

            if (slideInElement)
            {
                newDialogueElementContainer.GetComponent<DialogueElementContainerController>().SlideInElement();
            }
        }

        public void AddContinueBox(bool slideInElement)
        {
            Debug.Log("----- NEW CONTINUE BOX -----");

            GameObject newDialogueElementContainer = InstantiateDialogueElementContainer();
            GameObject newDialogueElement = InstantiateContinueBox();
            ParentDialogueElementToSelf(newDialogueElementContainer);
            ParentDialogueElementToContainer(newDialogueElement, newDialogueElementContainer);
            AddDialogueElementToElementList(newDialogueElementContainer);

            newDialogueElement.GetComponent<ContinueBoxController02>().InitializeElement();
            newDialogueElementContainer.GetComponent<DialogueElementContainerController>().InitializeElement();

            if (slideInElement)
            {
                newDialogueElementContainer.GetComponent<DialogueElementContainerController>().SlideInElement();
            }
        }

        public void AddResponseList(bool slideInElement)
        {
            Debug.Log("----- NEW RESPONSE LIST -----");

            GameObject newDialogueElementContainer = InstantiateDialogueElementContainer();
            GameObject newDialogueElement = InstantiateResponseListContainer();
            ParentDialogueElementToSelf(newDialogueElementContainer);
            ParentDialogueElementToContainer(newDialogueElement, newDialogueElementContainer);
            AddDialogueElementToElementList(newDialogueElementContainer);

            newDialogueElement.GetComponent<ResponseListContainerController02>().InitializeElement();
            newDialogueElementContainer.GetComponent<DialogueElementContainerController>().InitializeElement();

            if (slideInElement)
            {
                newDialogueElementContainer.GetComponent<DialogueElementContainerController>().SlideInElement();
            }
        }
        */

        private void ParentDialogueElementToSelf(GameObject dialogueElement)
        {
            dialogueElement.transform.SetParent(gameObject.transform, false);
        }

        private void ParentDialogueElementToContainer(GameObject dialogueElement, GameObject parentContainer)
        {
            dialogueElement.transform.SetParent(parentContainer.transform, false);
        }

        private void AddDialogueElementToElementList(GameObject dialogueElement)
        {
            dialogueElementList.Add(dialogueElement);
        }

        private GameObject InstantiateDialogueElement(GameObject dialogueElementPrefab)
        {
            GameObject newDialogueElement = GameObject.Instantiate(dialogueElementPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            return newDialogueElement;
        }

        private GameObject InstantiateDialogueElementContainer()
        {
            GameObject newDialogueElementContainer = GameObject.Instantiate(dialogueElementContainerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            //ParentDialogueElementToSelf(newDialogueElementContainer);
            return newDialogueElementContainer;
        }

        /*
        private GameObject InstantiateDialogueBox()
        {
            GameObject newDialogueElement = GameObject.Instantiate(dialougeBoxPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            //ParentDialogueElementToSelf(newDialogueElement);
            return newDialogueElement;
        }

        private GameObject InstantiateContinueBox()
        {
            GameObject newDialogueElement = GameObject.Instantiate(continueBoxPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            return newDialogueElement;
        }

        private GameObject InstantiateResponseListContainer()
        {
            GameObject newDialogueElement = GameObject.Instantiate(responseListContainerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            return newDialogueElement;
        }
        */

        /*
        public void RunTestDelegate()
        {
            DialogueElementUtilities.SlideInCallBack testDelegate = TestMethod;
            DialogueElementUtilities.Test(testDelegate);
        }

        public void TestMethod()
        {
            Debug.Log("The Test Delegate was passed successfully");
        }
        */
    }

    [CustomEditor(typeof(DialogueMainTimelineContainer))]
    public class DialogueContainerControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {


            DrawDefaultInspector();

            if (GUILayout.Button("Add Dialogue Box"))
            {
                Selection.activeGameObject.GetComponent<DialogueMainTimelineContainer>().AddDialogueElement(DialogueMainTimelineContainer.Instance.DialogueBoxPrefab, DialogueMainTimelineContainer.Instance.TestArticyRef.GetObject());
            }

            if (GUILayout.Button("Add Continue Box"))
            {
                Selection.activeGameObject.GetComponent<DialogueMainTimelineContainer>().AddDialogueElement(DialogueMainTimelineContainer.Instance.ContinueBoxPrefab, DialogueMainTimelineContainer.Instance.TestArticyRef.GetObject());
            }

            if (GUILayout.Button("Add Response List"))
            {
                Selection.activeGameObject.GetComponent<DialogueMainTimelineContainer>().AddDialogueElement(DialogueMainTimelineContainer.Instance.ResponseListContainerPrefab, DialogueMainTimelineContainer.Instance.TestArticyRef.GetObject());
            }

            //if (GUILayout.Button("Test"))
            //{
            //    Selection.activeGameObject.GetComponent<DialogueContainerController02>().AddDialogueElement(DialogueContainerController02.Instance.DialogueBoxPrefab, DialogueContainerController02.Instance.TestArticyRef.GetObject(), true);
            //}
        }
    }
}


