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

        private RectTransform rectTransform;
        private GameObject childElement;

        public GameObject Child { get => childElement; }

        void Awake()
        {
            SetReferences();
        }

        public void InitializeElement()
        {
            ResizeContainer();
        }

        private void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            childElement = DialogueElementUtilities.GetChildElement(gameObject);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public void ResizeContainer()
        {
            rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);
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

