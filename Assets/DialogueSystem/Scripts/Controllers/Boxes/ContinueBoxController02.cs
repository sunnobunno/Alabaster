using Articy.Unity;
using Assets.DialogueSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Dialogue_System.Controllers
{
    public class ContinueBoxController02 : MonoBehaviour, IDialogueElementClickable, IDialogueElementController<IFlowObject>
    {
        
        public static event Action SendClickedSignal;

        [Header("Content")]
        [SerializeField] private string content;

        [Header("Child Element References")]
        [SerializeField] private GameObject contentObject;

        private RectTransform rectTransform;
        private RectTransform contentRectTransform;
        private TextMeshProUGUI contentTextMesh;

        private IFlowObject aObject;
        private bool componentReferencesSet = false;

        private void Awake()
        {
            if (!componentReferencesSet)
            {
                SetElementComponentReferences();
            }
        }

        private void Update()
        {
            contentTextMesh.text = content;
        }

        /*
        public void InitializeElement()
        {
            SetElementComponentReferences();
            SetElementFont();
            SetElementSizeDelta();
        }

        public void InitializeElement(string content)
        {
            SetElementComponentReferences();
            SetElementContent(content);
            SetElementFont();
            SetElementSizeDelta();
        }
        */

        public void InitializeElement(IFlowObject aObject)
        {
            SetElementComponentReferences();
            SetElementContent(aObject);
            SetElementFont();
            SetElementSizeDelta();
        }



        public void SetElementContent(string content)
        {
            contentTextMesh.text = content;
            this.content = content;
        }

        public void SetElementContent(IFlowObject aObject)
        {
            //
        }

        protected void SetElementComponentReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            contentRectTransform = contentObject.GetComponent<RectTransform>();
            contentTextMesh = contentObject.GetComponentInChildren<TextMeshProUGUI>();

            content = contentTextMesh.text;
            componentReferencesSet = true;
        }

        protected void SetElementFont()
        {
            contentTextMesh.color = Color.gray;
            //contentTextMesh.font = DialogueUIController02.Instance.Font;
        }

        protected void SetElementSizeDelta()
        {
            contentRectTransform.sizeDelta = new Vector2(contentTextMesh.preferredWidth, contentTextMesh.preferredHeight);
            rectTransform.sizeDelta = new Vector2(contentTextMesh.preferredWidth, contentTextMesh.preferredHeight);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            contentTextMesh.color = Color.yellow;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            contentTextMesh.color = Color.gray;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Destroy(gameObject.transform.parent.gameObject);
            SendClickedSignal?.Invoke();
        }

        public void ResizeElement()
        {

        }

        public void GreyOutElement(bool isGrey)
        {
            //throw new NotImplementedException();
        }
    }
}