using Articy.Little_Guy_Syndrome;
using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Alabaster.DialogueSystem.Utilities;
using UnityEditor.Build.Content;
using Unity.VisualScripting;

namespace Alabaster.DialogueSystem.Controllers
{
    public class TextBoxSmallController : DialogueElement, IDialogueElementControllerWithContent
    {
        [SerializeField] private GameObject contentObject;
        [SerializeField] private GameObject centerObject;
        [SerializeField] private float contentMinWidth;

        private TextMeshProUGUI contentTextMesh;
        private IDialogueElementControllerWithContent contentObjectController;
        private RectTransform contentObjectRectTransform;
        private RectTransform centerObjectRectTransform;
        private RectTransform rectTransform;

        //private float contentMarginLeft;
        //private float contentMarginRight;

        public string Content
        {
            get => contentObjectController.Content;
            set => contentObjectController.Content = value;
        }

        public UnityEngine.Color TextColor
        {
            get => contentObjectController.TextColor;
            set => contentObjectController.TextColor = value;

        }

        protected override void Awake()
        {
            SetReferences();
        }

        protected override void SetFields()
        {
            
        }

        public void InitializeElement(string initializeData)
        {
            SetContent(initializeData);
        }

        protected override void SetReferences()
        {
            contentTextMesh = contentObject.GetComponent<TextMeshProUGUI>();
            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
            contentObjectRectTransform = contentObject.GetComponent<RectTransform>();
            centerObjectRectTransform = centerObject.GetComponent<RectTransform>();
            rectTransform = GetComponent<RectTransform>();

            //contentMarginLeft = contentObjectRectTransform.transform.localPosition.x;
            //contentMarginRight = contentObjectRectTransform.transform.localPosition.x;
        }

        public override void ResizeElement()
        {
            float contentPreferredWidth = contentTextMesh.preferredWidth;

            contentObjectRectTransform.sizeDelta = new Vector2(contentPreferredWidth, contentObjectRectTransform.sizeDelta.y);
            centerObjectRectTransform.sizeDelta = new Vector2(contentPreferredWidth, centerObjectRectTransform.sizeDelta.y);

            //Debug.Log(contentObjectRectTransform.sizeDelta);
            //Debug.Log(centerObjectRectTransform.sizeDelta);

            GetComponent<RectTransform>().sizeDelta = new Vector2(contentPreferredWidth, rectTransform.sizeDelta.y);

            isResized = true;

            Debug.Log($"{gameObject.name}: resized: {GetComponent<RectTransform>().sizeDelta}");
        }

        public void SetContent(string content)
        {
            Content = content;
            ResizeElement();
        }

        public override void GreyOut(bool isGrey)
        {
            contentObjectController.GreyOut(isGrey);
        }
    }
}


