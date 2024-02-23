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
        private bool isResized = false;

        public GameObject Child { get => childElement; }
        public bool IsResized { get { return isResized; } }

        public void InitializeElement()
        {
            SetReferences();
            ResizeContainer();
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

        public void ResizeContainer()
        {
            CallBacks.VoidCallBackWithGameObject callBack = SetResizedTrue;
            
            ElementResizer.EndOfFrameResizeElementByChildrenSizeDelta(this, callBack);
            
            //rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);
        }

        private void SetResizedTrue(GameObject gameObject)
        {
            isResized = true;
            //Debug.Log(isResized);
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
                Selection.activeGameObject.GetComponent<BoxContainer>().ResizeContainer();
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

