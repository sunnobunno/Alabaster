using Articy.Unity;
using Alabaster.DialogueSystem.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Experimental.AI;
using System.Runtime.CompilerServices;

namespace Alabaster.DialogueSystem.Controllers
{
    public class DialogueMainTimelineContainer : MonoBehaviour
    {

        public static event Action<Branch> SendResponseSignal;
        public static event Action<IFlowObject> SendContinueSignal;
        public static event Action SendSlideInEndSignal;

        public static DialogueMainTimelineContainer Instance { get; private set; }

        [SerializeField] private GameObject boxContainerPrefab;
        [SerializeField] private GameObject dialougeBoxPrefab;
        [SerializeField] private GameObject continueBoxPrefab;
        [SerializeField] private GameObject responseListContainerPrefab;
        [SerializeField] private GameObject choiceBoxPrefab;
        [SerializeField] private ArticyRef testArticyRef;


        #region Properties
        public GameObject DialogueElementContainerPrefab { get => boxContainerPrefab; }
        public GameObject DialogueBoxPrefab { get => dialougeBoxPrefab; }
        public GameObject ContinueBoxPrefab { get => continueBoxPrefab; }
        public GameObject ResponseListContainerPrefab { get => responseListContainerPrefab; }
        public ArticyRef TestArticyRef { get => testArticyRef; }
        public float ElementCount { get => dialogueElementList.Count; }
        public List<BoxContainer> ElementList { get => dialogueElementList; }
        public bool IsElementSlideDone { get => slideInEndSignalRecieved; set => slideInEndSignalRecieved = value; }
        public BoxContainer LastElement { get => dialogueElementList.Last(); }
        public float EaseInSpeed { get => easeInSpeed; }
        public float AutoScrollSpeed { get => autoScrollSpeed; }
        #endregion



        private List<BoxContainer> dialogueElementList = new();
        private bool slideInEndSignalRecieved = false;
        private bool resizeBuffer = false;
        private RectTransform rectTransform;
        private VerticalLayoutGroup verticalLayoutGroup;
        private bool isTimeLineResized = false;
        private float easeInSpeed = 0.5f;
        private float autoScrollSpeed = 0.5f;
        private string lastTitle = "";







        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            SetReferences();
        }

        private void Update()
        {
            ManualScroll();
        }

        private void SetReferences()
        {
            rectTransform = GetComponent<RectTransform>();
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
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
            SendResponseSignal?.Invoke(branch);
        }

        private void ListenContinueSignal(IFlowObject aObject)
        {
            Debug.Log("Continue Clicked");
            SendContinueSignal?.Invoke(aObject);
        }

        private void ListenSlideInEndSignal()
        {
            //Debug.Log("SlideInEndSignal Recieved");
            slideInEndSignalRecieved = true;
            SendSlideInEndSignal?.Invoke();
        }






        public void AddDialogueBox(IFlowObject aObject)
        {
            var currentTitle = ArticyConversions.IFlowObjectToTitle(aObject);
            
            AddDialogueElement(DialogueBoxPrefab, aObject);

            if (currentTitle == lastTitle)
            {
                LastElement.Child.GetComponent<DialogueBoxController>().ToggleTitle(false);
            }
            
            lastTitle = currentTitle;
        }

        public void AddContinueBox(IFlowObject aObject)
        {
            AddDialogueElement(ContinueBoxPrefab, aObject);
        }

        public void AddChoiceList(IFlowObject aObject)
        {
            AddDialogueElement(ResponseListContainerPrefab, aObject);
        }

        public void AddChoiceBoxCopy(Branch branch)
        {
            AddDialogueElement(choiceBoxPrefab, branch);
        }

        // The goal of this class is to create an interface to perform all the requirements
        // You should be able to add dialogue elements
        // Control the display of their title cards, grey them out, 


        public void AddDialogueElement<T>(GameObject dialogueElementPrefab, T aObject)
        {
            isTimeLineResized = false;

            string? aText = "";
            aText = ArticyConversions.AnyToText(aObject);

            Debug.Log($"----- NEW {dialogueElementPrefab.name}: {aText} -----");

            GameObject newBoxContainer = InstantiateBoxContainer();
            GameObject newDialogueElement = InstantiateDialogueElement(dialogueElementPrefab);
            ParentDialogueElementToSelf(newBoxContainer);
            ParentDialogueElementToContainer(newDialogueElement, newBoxContainer);
            dialogueElementList.Add(newBoxContainer.GetComponent<BoxContainer>());
            //AddDialogueElementToElementList(newBoxContainer);

            //newDialogueElement.GetComponent<IDialogueElementController<IFlowObject>>().InitializeElement(aObject);
            newBoxContainer.GetComponent<BoxContainer>().InitializeElement<T>(aObject);
        }


        //public void AddDialogueElement(GameObject dialogueElementPrefab, IFlowObject aObject)
        //{
        //    isTimeLineResized = false;
            
        //    string aText = "";
            
        //    if (ArticyConversions.IFlowObjectToText(aObject) != null)
        //    {
        //        aText = ArticyConversions.IFlowObjectToText(aObject);
        //    }
            
        //    Debug.Log($"----- NEW {dialogueElementPrefab.name}: {aText} -----");

        //    GameObject newBoxContainer = InstantiateBoxContainer();
        //    GameObject newDialogueElement = InstantiateDialogueElement(dialogueElementPrefab);
        //    ParentDialogueElementToSelf(newBoxContainer);
        //    ParentDialogueElementToContainer(newDialogueElement, newBoxContainer);
        //    dialogueElementList.Add(newBoxContainer.GetComponent<BoxContainer>());
        //    //AddDialogueElementToElementList(newBoxContainer);

        //    //newDialogueElement.GetComponent<IDialogueElementController<IFlowObject>>().InitializeElement(aObject);
        //    newBoxContainer.GetComponent<BoxContainer>().InitializeElement<IFlowObject>(aObject);

        //    //DialogueElementController controller = dialogueElementPrefab.GetComponent<DialogueElementController>();
        //}

        //public void AddDialogueElement(GameObject dialogueElementPrefab, Branch branch)
        //{
        //    isTimeLineResized = false;

        //    string aText = "";

        //    if (ArticyConversions.BranchToText(branch) != null)
        //    {
        //        aText = ArticyConversions.BranchToText(branch);
        //    }

        //    Debug.Log($"----- NEW {dialogueElementPrefab.name}: {aText} -----");

        //    GameObject newBoxContainer = InstantiateBoxContainer();
        //    GameObject newDialogueElement = InstantiateDialogueElement(dialogueElementPrefab);
        //    ParentDialogueElementToSelf(newBoxContainer);
        //    ParentDialogueElementToContainer(newDialogueElement, newBoxContainer);
        //    dialogueElementList.Add(newBoxContainer.GetComponent<BoxContainer>());
        //    //AddDialogueElementToElementList(newBoxContainer);

        //    newDialogueElement.GetComponent<IDialogueElementController<Branch>>().InitializeElement(branch);
        //    newBoxContainer.GetComponent<BoxContainer>().InitializeElement();

        //    //DialogueElementController controller = dialogueElementPrefab.GetComponent<DialogueElementController>();
        //}






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
            //dialogueElementList.Add(dialogueElement);
        }

        private GameObject InstantiateDialogueElement(GameObject dialogueElementPrefab)
        {
            var prefaxY = 0f;
            var prefabX = dialogueElementPrefab.GetComponent<RectTransform>().localPosition.x;
            var prefabStartingPosition = new Vector3(prefabX, prefaxY, 0f);
            
            GameObject newDialogueElement = GameObject.Instantiate(dialogueElementPrefab, prefabStartingPosition, Quaternion.identity);
            return newDialogueElement;
        }

        private GameObject InstantiateBoxContainer()
        {
            GameObject newDialogueElementContainer = GameObject.Instantiate(boxContainerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            return newDialogueElementContainer;
        }




        public void ResizeContainer()
        {
            var resizeContainer = CoResizeContainer();
            StartCoroutine(resizeContainer);
        }

        private IEnumerator CoResizeContainer()
        {
            isTimeLineResized = false;
            
            while (!LastElement.IsResized)
            {
                yield return null;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            rectTransform.sizeDelta = new Vector2(verticalLayoutGroup.preferredWidth, verticalLayoutGroup.preferredHeight);
            //Debug.Log($"TimeLine Resized: {verticalLayoutGroup.preferredHeight}");

            isTimeLineResized = true;
        }






        private Vector3 GetTargetPosition()
        {
            float bottomOfContainer = rectTransform.localPosition.y - rectTransform.sizeDelta.y;
            float x = rectTransform.localPosition.x;
            float y = rectTransform.sizeDelta.y + DialogueUIController.Instance.AutoScrollBottomThreshhold;

            if (bottomOfContainer > DialogueUIController.Instance.AutoScrollBottomThreshhold)
            {
                y = rectTransform.localPosition.y;
            }

            Vector3 targetPosition = new Vector3(x, y);
            return targetPosition;
        }

        public void AutoScrollContainer()
        {
            IEnumerator coAutoScrollContainer = CoAutoScrollContainer();
            StartCoroutine(coAutoScrollContainer);
        }

        private IEnumerator CoAutoScrollContainer()
        {
            while (!isTimeLineResized)
            {
                yield return null;
            }
            
            CallBacks.VoidCallBack callBack = AutoScrollEndCallBack;
            Vector3 targetPosition = GetTargetPosition();

            ElementScroller.EaseElementToTargetPosition(gameObject, targetPosition, callBack, this);
        }

        private void AutoScrollEndCallBack()
        {

        }

        private void ManualScroll()
        {
            
            Vector3 scrollDelta = new Vector3(0f, Input.mouseScrollDelta.y * DialogueUIController.Instance.ScrollSpeed * -1f);
            //Debug.Log(scrollDelta);
            rectTransform.localPosition += scrollDelta;
        }
    }




    [CustomEditor(typeof(DialogueMainTimelineContainer))]
    public class DialogueContainerControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var selection = Selection.activeGameObject.GetComponent<DialogueMainTimelineContainer>();

            DrawDefaultInspector();

            if (GUILayout.Button("Add Dialogue Box"))
            {
                selection.AddDialogueElement(selection.DialogueBoxPrefab, selection.TestArticyRef.GetObject());
            }

            if (GUILayout.Button("Add Continue Box"))
            {
                selection.AddDialogueElement(selection.ContinueBoxPrefab, selection.TestArticyRef.GetObject());
            }

            if (GUILayout.Button("Add Response List"))
            {
                selection.AddDialogueElement(selection.ResponseListContainerPrefab, selection.TestArticyRef.GetObject());
            }

            //if (GUILayout.Button("Test"))
            //{
            //    Selection.activeGameObject.GetComponent<DialogueContainerController02>().AddDialogueElement(DialogueContainerController02.Instance.DialogueBoxPrefab, DialogueContainerController02.Instance.TestArticyRef.GetObject(), true);
            //}
        }
    }
}


