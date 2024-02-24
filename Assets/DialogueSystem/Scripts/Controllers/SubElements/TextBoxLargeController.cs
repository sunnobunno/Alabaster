using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Alabaster.DialogueSystem.Utilities;

namespace Alabaster.DialogueSystem.Controllers
{
    public class TextBoxLargeController : DialogueElement, IDialogueElementControllerWithContent
    {

        [SerializeField] private TextBoxLargeImageAssets textBoxImageAssets;
        [SerializeField] private GameObject contentObject;
        [SerializeField] private GameObject centerObject;
        [SerializeField] private GameObject topObject;
        [SerializeField] private GameObject leftObject;
        [SerializeField] private GameObject rightObject;
        [SerializeField] private GameObject bottomObject;
        [SerializeField] private GameObject topLeftObject;
        [SerializeField] private GameObject topRightObject;
        [SerializeField] private GameObject bottomLeftObject;
        [SerializeField] private GameObject bottomRightObject;

        private TextMeshProUGUI contentTextMesh;
        private IDialogueElementControllerWithContent textObjectController;
        private RectTransform contentObjectRectTransform;
        private RectTransform centerObjectRectTransform;
        private RectTransform topObjectRectTransform;
        private RectTransform leftObjectRectTransform;
        private RectTransform rightObjectRectTransform;
        private RectTransform bottomObjectRectTransform;
        private RectTransform topLeftObjectRectTransform;
        private RectTransform topRightObjectRectTransform;
        private RectTransform bottomLeftObjectRectTransform;
        private RectTransform bottomRightObjectRectTransform;

        private Image centerObjectImage;
        private Image topObjectImage;
        private Image leftObjectImage;
        private Image rightObjectImage;
        private Image bottomObjectImage;
        private Image topLeftObjectImage;
        private Image topRightObjectImage;
        private Image bottomLeftObjectImage;
        private Image bottomRightObjectImage;

        public TextBoxLargeImageAssets TextBoxImageAssets { get => textBoxImageAssets;
            set
                {
                    textBoxImageAssets = value;
                    SetTextBoxImageAssets();
                }
        }

        public string Content
        {
            get => textObjectController.Content;
            set => textObjectController.Content = value;
        }

        public UnityEngine.Color TextColor
        {
            get => textObjectController.TextColor;
            set => textObjectController.TextColor = value;
        }

        protected override void Awake()
        {
            SetReferences();
        }

        protected override void Start()
        {
            SetFields();
        }

        public void InitializeElement(string content)
        {
            SetContent(content);
            ResizeElement();
        }



        protected override void SetReferences()
        {
            contentTextMesh = contentObject.GetComponent<TextMeshProUGUI>();
            textObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();

            contentObjectRectTransform = contentObject.GetComponent<RectTransform>();
            centerObjectRectTransform = centerObject.GetComponent<RectTransform>();
            topObjectRectTransform = topObject.GetComponent<RectTransform>();
            leftObjectRectTransform = leftObject.GetComponent<RectTransform>();
            rightObjectRectTransform = rightObject.GetComponent<RectTransform>();
            bottomObjectRectTransform = bottomObject.GetComponent<RectTransform>();
            topLeftObjectRectTransform = topLeftObject.GetComponent<RectTransform>();
            topRightObjectRectTransform = topRightObject.GetComponent<RectTransform>();
            bottomLeftObjectRectTransform = bottomLeftObject.GetComponent<RectTransform>();
            bottomRightObjectRectTransform = bottomRightObject.GetComponent<RectTransform>();

            centerObjectImage = centerObject.GetComponent<Image>();
            topObjectImage = topObject.GetComponent<Image>();
            leftObjectImage = leftObject.GetComponent<Image>();
            rightObjectImage = rightObject.GetComponent<Image>();
            bottomObjectImage = bottomObject.GetComponent<Image>();
            topLeftObjectImage = topLeftObject.GetComponent<Image>();
            topRightObjectImage = topRightObject.GetComponent<Image>();
            bottomLeftObjectImage = bottomLeftObject.GetComponent<Image>();
            bottomRightObjectImage = bottomRightObject.GetComponent<Image>();
        }

        protected override void SetFields()
        {
            SetTextBoxImageAssets();
        }

        private void SetTextBoxImageAssets()
        {
            //Debug.Log(textBoxImageAssets.center.name);

            centerObjectImage.sprite = textBoxImageAssets.center;
            topObjectImage.sprite = textBoxImageAssets.top;
            leftObjectImage.sprite = textBoxImageAssets.left;
            rightObjectImage.sprite = textBoxImageAssets.right;
            bottomObjectImage.sprite = textBoxImageAssets.bottom;
            topLeftObjectImage.sprite = textBoxImageAssets.topLeft;
            topRightObjectImage.sprite = textBoxImageAssets.topRight;
            bottomLeftObjectImage.sprite = textBoxImageAssets.bottomLeft;
            bottomRightObjectImage.sprite = textBoxImageAssets.bottomRight;
        }

        public void SetContent(string content)
        {
            Content = content;
            ResizeElement();
        }

        public override void ResizeElement()
        {
            ResizeSubElements();
            GetComponent<RectTransform>().sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(this.gameObject);
            isResized = true;

            Debug.Log($"{gameObject.name}: resized");
        }

        public void ResizeSubElements()
        {
            contentObject.GetComponent<ContentBoxController>().ResizeElement();
            Vector2 contentObjectSize = contentObject.GetComponent<RectTransform>().sizeDelta;

            //Debug.Log(contentObjectSize);

            centerObjectRectTransform.sizeDelta = contentObjectSize;
            leftObjectRectTransform.sizeDelta = new Vector2(leftObjectRectTransform.sizeDelta.x, contentObjectSize.y);
            rightObjectRectTransform.sizeDelta = new Vector2(rightObjectRectTransform.sizeDelta.x, contentObjectSize.y);

            bottomObjectRectTransform.localPosition = new Vector2(bottomObjectRectTransform.localPosition.x, (topObjectRectTransform.sizeDelta.y + contentObjectSize.y) * -1f);
            bottomLeftObjectRectTransform.localPosition = new Vector2(bottomLeftObjectRectTransform.localPosition.x, (topObjectRectTransform.sizeDelta.y + contentObjectSize.y) * -1f);
            bottomRightObjectRectTransform.localPosition = new Vector2(bottomRightObjectRectTransform.localPosition.x, (topObjectRectTransform.sizeDelta.y + contentObjectSize.y) * -1f);
        }

        public override void GreyOut(bool isGrey)
        {
            textObjectController.GreyOut(isGrey);
        }
    }

    [CustomEditor(typeof(TextBoxLargeController))]
    public class BoxBackgroundControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Resize Box"))
            {
                Selection.activeGameObject.GetComponent<TextBoxLargeController>().ResizeElement();
            }
        }
    }
}


