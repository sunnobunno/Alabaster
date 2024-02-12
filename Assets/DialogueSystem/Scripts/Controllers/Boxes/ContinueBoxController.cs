using Articy.Unity;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alabaster.DialogueSystem.Controllers
{
    public class ContinueBoxController : MonoBehaviour, IDialogueElementClickable, IDialogueElementController<IFlowObject>
    {
        
        public static event Action SendClickedSignal;

        [Header("Content")]
        [SerializeField] private string content;
        [Header("Child Objects")]
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
            SetFields();
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetContent(aObject);
        }

        

        public void SetContent(IFlowObject aObject)
        {
            //
        }

        private void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
        }

        private void SetFields()
        {
            contentObjectController.TextColor = Color.grey;
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