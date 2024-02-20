using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alabaster.DialogueSystem.Utilities
{
    public static class ElementResizer
    {
        public static void EndOfFrameResizeElementByChildrenSizeDelta(MonoBehaviour callingObject)
        {
            var gameObject = callingObject.gameObject;
            CallBacks.VoidCallBackWithGameObject callBack = ResizeElementByChildrenSizeDelta;
            IEnumerator callBackAtEndOfFrame = CallBacks.CoCallBackAtEndOfFrame(callBack, gameObject);
            callingObject.StartCoroutine(callBackAtEndOfFrame);
        }

        public static void EndOfFrameResizeElementByChildrenSizeDelta(MonoBehaviour callingObject, CallBacks.VoidCallBackWithGameObject additionalCallback)
        {
            var gameObject = callingObject.gameObject;
            CallBacks.VoidCallBackWithGameObject callBack = ResizeElementByChildrenSizeDelta;
            callBack += additionalCallback;
            IEnumerator callBackAtEndOfFrame = CallBacks.CoCallBackAtEndOfFrame(callBack, gameObject);
            callingObject.StartCoroutine(callBackAtEndOfFrame);
        }

        public static void ResizeElementByChildrenSizeDelta(GameObject gameObject)
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);

            string debugText = "";
            if (gameObject.GetComponent<DialogueBoxController>() != null)
            {
                debugText = gameObject.GetComponent<DialogueBoxController>().Content;
            }
            else
            {
                debugText = gameObject.name;
            }

            if (gameObject.GetComponent<BoxContainer>() != null)
            {

            }

            Debug.Log($"{gameObject.name}: resized: {debugText}");
        }
    }
}


