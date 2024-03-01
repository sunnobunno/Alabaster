using Alabaster.DialogueSystem.Utilities;
using Alabaster.EnvironmentSystem;
using Articy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Alabaster.DialogueSystem.Controllers
{
    public class BoxContainer : DialogueElement
    {
        [SerializeField] public ArticyRef testArticyRef;

        public static event Action SendSlideInEndSignal;
        public static event Action SendResizeEndSignal;

        private RectTransform rectTransform;
        private GameObject childElement;
        private DialogueElement childDialogueElementController;
        private CanvasGroup canvasGroup;
        private ParallaxElementUI? parallaxContainer;

        public GameObject Child { get => childElement; }

        protected override void Awake()
        {
            
        }

        protected override void Start()
        {
            
        }

        public void InitializeElement()
        {
            SetReferences();
            ResizeElement();
        }

        public void InitializeElement<T>(T data)
        {
            SetReferences();
            InitializeChildElement<T>(data);
            ResizeElement();
        }

        protected override void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            childElement = DialogueElementUtilities.GetChildElement(gameObject);
            canvasGroup = GetComponent<CanvasGroup>();
            childDialogueElementController = DialogueElementUtilities.GetChildDialogueElementController(gameObject);
            childElement.TryGetComponent<ParallaxElementUI>(out parallaxContainer);
            Debug.Log($"{gameObject.name}: child: {childDialogueElementController.gameObject.name}");
        }

        private void InitializeChildElement<T>(T data)
        {
            (childDialogueElementController as IDialogueElementController<T>).InitializeElement(data);

        }

        protected override void SetFields()
        {
            
        }

        public void DestroySelf()
        {
            Debug.Log($"{gameObject.name}: Destroying self: {childElement.name}");
            Destroy(gameObject);
        }

        public override void ResizeElement()
        {
            if (isResized) return;

            IEnumerator coResizeElement = CoResizeElement();
            StartCoroutine(coResizeElement);
        }

        private IEnumerator CoResizeElement()
        {
            //if (!Child.TryGetComponent<IDialogueElementClickable<IFlowObject>>(out _)) Hide();
            
            childDialogueElementController.ResizeElement();

            while (childDialogueElementController.IsResized == false)
            {
                yield return null;
            }

            CallBacks.VoidCallBackWithGameObject callBack = SetResizedTrue;
            ElementResizer.EndOfFrameResizeElementByChildrenSizeDelta(this, callBack);
        }

        protected override void SetResizedTrue(GameObject gameObject)
        {
            base.SetResizedTrue(gameObject);
            //if (!Child.TryGetComponent<IDialogueElementClickable<IFlowObject>>(out _)) Show();
        }

        public void SlideInElement()
        {
            CallBacks.VoidCallBack invokeSlideInEndSignal = InvokeSlideInEndSignal;
            ElementScroller.SlideInElementOffScreen(childElement, invokeSlideInEndSignal, this);
        }

        private void InvokeSlideInEndSignal()
        {
            SendSlideInEndSignal?.Invoke();
        }

        public override void GreyOut(bool isGrey)
        {
            //Debug.Log("Greying out");
            childElement.GetComponent<DialogueElement>()?.GreyOut(true);
        }

        public void Hide()
        {
            //Debug.Log($"{Child.name}: hiding");
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            //Debug.Log($"{Child.name}: showing");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }

    [CustomEditor(typeof(BoxContainer))]
    public class BoxContainerEditor : Editor
    {
        public override void OnInspectorGUI()
        {


            DrawDefaultInspector();

            if (GUILayout.Button("Resize Container"))
            {
                Selection.activeGameObject.GetComponent<BoxContainer>().ResizeElement();
            }
            if (GUILayout.Button("Slide In Element"))
            {
                Selection.activeGameObject.GetComponent<BoxContainer>().SlideInElement();
            }
            if (GUILayout.Button("Destroy"))
            {
                Selection.activeGameObject.GetComponent<BoxContainer>().DestroySelf();
            }
            if (GUILayout.Button("Initialize"))
            {
                Selection.activeGameObject.GetComponent<BoxContainer>().InitializeElement<IFlowObject>(Selection.activeGameObject.GetComponent<BoxContainer>().testArticyRef.GetObject());
            }
        }
    }
}

