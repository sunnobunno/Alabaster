using Articy.Unity;
using Assets.DialogueSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Dialogue_System.Controllers
{
    public class ContinueBoxController : MonoBehaviour, IDialogueElementClickable, IDialogueElementController<IFlowObject>
    {
        
        public static event Action SendClickedSignal;

        [Header("Content")]
        [SerializeField] private string content;

        [Header("Child Element References")]
        [SerializeField] private GameObject contentObject;

        private RectTransform rectTransform;
        private IDialogueElementControllerWithContent contentObjectController;

        private IFlowObject aObject;
        private bool componentReferencesSet = false;

        private void Awake()
        {
            SetReferences();
        }

        private void Start()
        {
            contentObjectController.TextColor = Color.grey;
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetContent(aObject);
        }



        public void SetElementContent(string content)
        {
            //contentObjectController.Content = content;
            //this.content = content;
        }

        public void SetContent(IFlowObject aObject)
        {
            //
        }

        protected void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
        }

        protected void SetElementSizeDelta()
        {

        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            contentObjectController.TextColor = Color.yellow;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            contentObjectController.TextColor = Color.grey;
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