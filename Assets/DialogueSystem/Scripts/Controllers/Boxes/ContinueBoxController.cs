using Articy.Unity;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alabaster.DialogueSystem.Controllers
{
    public class ContinueBoxController : DialogueElement, IDialogueElementClickable<IFlowObject>, IDialogueElementController<IFlowObject>
    {
        
        public static event Action<IFlowObject> SendClickedSignal;

        [Header("Content")]
        [SerializeField] private string content;
        [Header("Child Objects")]
        [SerializeField] private GameObject contentObject;
        //[SerializeField] private Animator animator;
        //[SerializeField] private ContinueBoxBackground background;

        private RectTransform rectTransform;
        private IDialogueElementControllerWithContent contentObjectController;
        private bool hover = false;


        private IFlowObject aObject;

        private void Update()
        {
            //HandleHover();
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetContent(aObject);
        }

        

        public void SetContent(IFlowObject aObject)
        {
            this.aObject = aObject;
            ResizeElement();
        }

        protected override void SetReferences()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
        }

        protected override void SetFields()
        {
            contentObjectController.TextColor = Color.grey;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            hover = true;
            contentObjectController.TextColor = Color.yellow;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hover = false;
            contentObjectController.TextColor = Color.grey;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //Destroy(gameObject.transform.parent.gameObject);
            SendClickedSignal?.Invoke(aObject);
            DestroySelf();
        }

        //private void HandleHover()
        //{
        //    if (hover || background.Hover)
        //    {
        //        animator.SetBool("Hover", true);
        //    }
        //    else
        //    {
        //        animator.SetBool("Hover", false);
        //    }
        //}

        public override void ResizeElement()
        {
            if (isResized) { return; }
            
            SetResizedTrue(gameObject);
        }

        public override void GreyOut(bool isGrey)
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