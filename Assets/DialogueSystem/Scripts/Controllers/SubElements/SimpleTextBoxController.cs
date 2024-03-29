using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem.Controllers
{
    public class SimpleTextBoxController : DialogueElement, IDialogueElementControllerWithContent
    {
        //[SerializeField] private string content;

        private TextMeshProUGUI contentTextMesh;
        private RectTransform rectTransform;

        public string Content
        {
            get => contentTextMesh.text;
            set => contentTextMesh.text = value;
        }

        public UnityEngine.Color TextColor
        {
            get => contentTextMesh.color;
            set => contentTextMesh.color = value;

        }

        protected override void Awake()
        {
            SetReferences();
        }


        public void InitializeElement(string content)
        {

            SetContent(content);
            //ResizeElement();
        }

        protected override void SetReferences()
        {
            contentTextMesh = GetComponent<TextMeshProUGUI>();
            rectTransform = GetComponent<RectTransform>();
        }

        protected override void SetFields()
        {
            
        }

        public void SetContent(string content)
        {
            Content = content;
        }

        public override void ResizeElement()
        {
            Vector2 textMeshPreferredSize = contentTextMesh.GetPreferredValues();
            rectTransform.sizeDelta = new Vector2(DialogueUIController.Instance.DialogueWidth, textMeshPreferredSize.y);
            isResized = true;

            //Debug.Log($"{gameObject.name}: resized");
        }

        public override void GreyOut(bool isGrey)
        {
            if (isGrey)
            {
                contentTextMesh.color = Color.gray;
            }
            else
            {
                contentTextMesh.color = Color.white;
            }
        }
    }

    [CustomEditor(typeof(SimpleTextBoxController))]
    public class SimpleTextBoxControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var selection = Selection.activeGameObject.GetComponent<ContentBoxController>();

            if (GUILayout.Button("Resize Box"))
            {
                selection.ResizeElement();
            }
        }
    }
}


