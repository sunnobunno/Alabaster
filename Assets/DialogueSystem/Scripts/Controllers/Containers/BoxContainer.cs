using Alabaster.DialogueSystem.Utilities;
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

        public static event Action SendSlideInEndSignal;
        public static event Action SendResizeEndSignal;

        private RectTransform rectTransform;
        private GameObject childElement;
        private DialogueElement childDialogueElementController;
        private CanvasGroup canvasGroup;

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

        protected override void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            childElement = DialogueElementUtilities.GetChildElement(gameObject);
            canvasGroup = GetComponent<CanvasGroup>();
            childDialogueElementController = DialogueElementUtilities.GetChildDialogueElementController(gameObject);
            //Debug.Log($"{childDialogueElementController?.gameObject.name} hi");
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
            CallBacks.VoidCallBackWithGameObject callBack = SetResizedTrue;
            
            ElementResizer.EndOfFrameResizeElementByChildrenSizeDelta(this, callBack);
        }

        private void SetResizedTrue(GameObject gameObject)
        {
            isResized = true;
            Debug.Log($"{gameObject.name}: resized callback complete");
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
            Debug.Log("Greying out");
            childElement.GetComponent<DialogueElement>()?.GreyOut(true);
        }

        public void Hide()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
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
        }
    }
}

