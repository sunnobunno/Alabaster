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
            var initialPivot = gameObject.GetComponent<RectTransform>().pivot;
            var standardPivot = new Vector2(0f, 1f);
            gameObject.GetComponent<RectTransform>().pivot = standardPivot;

            var rectTransform = gameObject.GetComponent<RectTransform>();

            if (initialPivot == standardPivot)
            {
                rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);
                return;
            }

            
            var standardLocalPosition = rectTransform.anchoredPosition;
            var pivotDifference = standardPivot - initialPivot;
            var initialSizeDelta = rectTransform.sizeDelta;

            var xAdjust = initialSizeDelta.x * pivotDifference.x;
            var yAdjust = initialSizeDelta.y * pivotDifference.y;
            var positionAdjust = new Vector3(xAdjust, yAdjust);
            rectTransform.localPosition += positionAdjust;

            Debug.Log(positionAdjust);

            rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);

            //Debug.Log($"newSizeDelta: {rectTransform.sizeDelta}");

            gameObject.GetComponent<RectTransform>().pivot = initialPivot;
            xAdjust = -rectTransform.sizeDelta.x * pivotDifference.x;
            yAdjust = -rectTransform.sizeDelta.y * pivotDifference.y;
            positionAdjust = new Vector3(xAdjust, yAdjust);
            rectTransform.localPosition += positionAdjust;

            //var newPivotX = standardLocalPosition.x + (rectTransform.sizeDelta.x * pivotDifference.x);
            //var newPivotY = standardLocalPosition.y + (rectTransform.sizeDelta.y * pivotDifference.y);
            //var newLocalPosition = new Vector3(newPivotX, newPivotY);


            //rectTransform.localPosition = newLocalPosition;
            //gameObject.GetComponent<RectTransform>().pivot = initialPivot;

            Debug.Log(standardLocalPosition);
            Debug.Log(rectTransform.localPosition);

            //var pivotDifference = initialPivot - standardPivot;
            //gameObject.GetComponent<RectTransform>().localPosition += new Vector3(sizeOfChildren.x * pivotDifference.x, sizeOfChildren.y * pivotDifference.y);

            //Debug.Log(parent.GetComponent<RectTransform>().localPosition);
            //Debug.Log(initialPivot);
            //Debug.Log(pivotDifference);

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

            //Debug.Log($"{gameObject.name}: resized: {debugText}");
        }
    }
}


