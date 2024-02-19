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
    public class BoxContainer : MonoBehaviour
    {

        public static event Action SendSlideInEndSignal;
        public static event Action SendResizeEndSignal;

        private RectTransform rectTransform;
        private GameObject childElement;
        private CanvasGroup canvasGroup;
        private bool isResized = false;

        public GameObject Child { get => childElement; }
        public bool IsResized { get { return isResized; } }

        void Awake()
        {
            
        }

        public void InitializeElement()
        {
            SetReferences();
            ResizeContainer();
        }

        private void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            childElement = DialogueElementUtilities.GetChildElement(gameObject);
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void DestroySelf()
        {
            Debug.Log($"{gameObject.name}: Destroying self: {childElement.name}");
            Destroy(gameObject);
        }

        public void ResizeContainer()
        {
            DialogueElementUtilities.CallBackWithGameObject callBack = SetResizedTrue;
            
            DialogueElementUtilities.EndOfFrameResizeElementByChildrenSizeDelta(this, callBack);
            
            //rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);
        }

        private void SetResizedTrue(GameObject gameObject)
        {
            isResized = true;
            //Debug.Log(isResized);
        }

        public void SlideInElement()
        {
            DialogueElementUtilities.VoidCallBack invokeSlideInEndSignal = InvokeSlideInEndSignal;
            DialogueElementUtilities.SlideInElementOffScreen(childElement, invokeSlideInEndSignal, this);
        }

        private void InvokeSlideInEndSignal()
        {
            SendSlideInEndSignal?.Invoke();
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

