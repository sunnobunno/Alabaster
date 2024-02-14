using Articy.Unity;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alabaster.DialogueSystem.Controllers
{
    public class ContinueBoxController : MonoBehaviour, IDialogueElementClickable<IFlowObject>, IDialogueElementController<IFlowObject>
    {
        
        public static event Action<IFlowObject> SendClickedSignal;

        [Header("Content")]
        [SerializeField] private string content;
        [Header("Child Objects")]
        [SerializeField] private GameObject contentObject;

        private RectTransform rectTransform;
        private IDialogueElementControllerWithContent contentObjectController;

        private IFlowObject aObject;

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
            this.aObject = aObject;
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
            //Destroy(gameObject.transform.parent.gameObject);
            SendClickedSignal?.Invoke(aObject);
            DestroySelf();
        }

        public void ResizeElement()
        {

        }

        public void GreyOutElement(bool isGrey)
        {
            //throw new NotImplementedException();
        }

        public void DestroySelf()
        {
            if (transform.parent.GetComponent<BoxContainer>() != null)
            {
                transform.parent.GetComponent<BoxContainer>().DestroySelf();
            }
            else
            {
                Debug.Log($"{gameObject.name}: no BoxContainer parent.");
                Destroy(gameObject);
            }
        }
    }
}