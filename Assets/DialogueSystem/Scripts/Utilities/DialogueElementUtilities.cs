using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Alabaster.DialogueSystem.Utilities
{
    
    
    public static class DialogueElementUtilities
    {

        public delegate void VoidCallBack();
        public delegate void CallBackWithGameObject(GameObject gameObject);


        public static void SlideInElementOffScreen(GameObject gameObject, VoidCallBack slideInEndCallBack, MonoBehaviour callingObject)
        {
            SetElementOffScreen(gameObject);
            EaseInElement(gameObject, slideInEndCallBack, callingObject);
        }

        public static void SetElementOffScreen(GameObject gameObject)
        {
            float screenHeight = DialogueUIController.Instance.ScreenHeight;
            Vector3 elementLocalPosition = gameObject.GetComponent<RectTransform>().localPosition;
            Vector3 offScreenPosition = new(elementLocalPosition.x, elementLocalPosition.y - screenHeight);

            gameObject.GetComponent<RectTransform>().localPosition = offScreenPosition;
        }

        public static void EaseInElement(GameObject gameObject, VoidCallBack slideInEndCallBack, MonoBehaviour callingObject)
        {
            IEnumerator easeInChildElementCoroutine = DialogueElementUtilities.ParabolicMoveObjectRelative(gameObject, 0.5f, gameObject.GetComponent<RectTransform>().localPosition, Vector3.zero, slideInEndCallBack);
            callingObject.StartCoroutine(easeInChildElementCoroutine);
        }

        public static float GetObjectSizeDeltaY(GameObject gameObject)
        {
            return gameObject.GetComponent<RectTransform>().sizeDelta.y;
        }

        public static GameObject GetChildElement(GameObject gameObject)
        {
            GameObject childElement = gameObject.transform.GetChild(0).gameObject;
            return childElement;
        }

        public static void InheritHeightOfChild(GameObject gameObject)
        {
            var childObject = GetChildElement(gameObject);
            var rectTransform = gameObject.GetComponent<RectTransform>();
            var elementWidth = DialogueUIController.Instance.DialogueWidth;
            var elementHeight = GetObjectSizeDeltaY(childObject);
            rectTransform.sizeDelta = new Vector2(elementWidth, elementHeight);
        }

        public static float GetPreferredHeightOfElement(GameObject gameObject)
        {
            float preferredHeightOfElement = LayoutUtility.GetPreferredHeight(gameObject.GetComponent<RectTransform>());
            return preferredHeightOfElement;
        }

        public static IEnumerator ParabolicMoveObjectRelative(GameObject objectToMove, float durationInSeconds, Vector3 initialPosition, Vector3 targetPosition, VoidCallBack callBack)
        {
            //Debug.Log("pre-move object position: " + objectToMove.GetComponent<RectTransform>().localPosition);

            // how far to move
            float changeInX = targetPosition.x - initialPosition.x;
            float changeInY = targetPosition.y - initialPosition.y;

            float fps = 100f;
            // calculate the number of steps by the duration in seconds. a good rule of thumb is 0.5 s. 100 fps
            float steps = fps * durationInSeconds;
            float increment = 1f / steps;

            float prevXParabola = 0f;
            float prevYParabola = 0f;

            //Debug.Log(scrollDelta);

            for (float i = 0f; i < 1f; i += increment)
            {
                float xParabola = ParabolicEase(i, Mathf.Abs(changeInX)) * Mathf.Sign(changeInX);
                float xParabolaDelta = xParabola - prevXParabola;
                prevXParabola = xParabola;

                float yParabola = ParabolicEase(i, Mathf.Abs(changeInY)) * Mathf.Sign(changeInY);
                float yParabolaDelta = yParabola - prevYParabola;
                prevYParabola = yParabola;

                //Debug.Log(yParabolaDelta);

                Vector3 deltaStepVector = new Vector3(xParabolaDelta, yParabolaDelta, 0f);

                objectToMove.GetComponent<RectTransform>().localPosition += deltaStepVector;

                //Debug.Log("object position: " + objectToMove.GetComponent<RectTransform>().localPosition);

                yield return new WaitForSeconds(1f / fps);
            }

            callBack?.Invoke();
        }

        public static IEnumerator ParabolicMoveObjectRelative(GameObject objectToMove, float durationInSeconds, Vector3 initialPosition, Vector3 targetPosition)
        {
            //Debug.Log("pre-move object position: " + objectToMove.GetComponent<RectTransform>().localPosition);

            // how far to move
            float changeInX = targetPosition.x - initialPosition.x;
            float changeInY = targetPosition.y - initialPosition.y;

            float fps = 100f;
            // calculate the number of steps by the duration in seconds. a good rule of thumb is 0.5 s. 100 fps
            float steps = fps * durationInSeconds;
            float increment = 1f / steps;

            float prevXParabola = 0f;
            float prevYParabola = 0f;

            //Debug.Log(scrollDelta);

            for (float i = 0f; i < 1f; i += increment)
            {
                float xParabola = ParabolicEase(i, Mathf.Abs(changeInX)) * Mathf.Sign(changeInX);
                float xParabolaDelta = xParabola - prevXParabola;
                prevXParabola = xParabola;

                float yParabola = ParabolicEase(i, Mathf.Abs(changeInY)) * Mathf.Sign(changeInY);
                float yParabolaDelta = yParabola - prevYParabola;
                prevYParabola = yParabola;

                //Debug.Log(yParabolaDelta);

                Vector3 deltaStepVector = new Vector3(xParabolaDelta, yParabolaDelta, 0f);

                objectToMove.GetComponent<RectTransform>().localPosition += deltaStepVector;

                //Debug.Log("object position: " + objectToMove.GetComponent<RectTransform>().localPosition);

                yield return new WaitForSeconds(1f / fps);
            }
        }

        private static float ParabolicEase(float x, float delta)
        {
            float y = (-1f * delta * x * x) + (2f * delta * x);

            //float y = (-2f * delta) * (x + 1);

            return y;
        }

        public static void CallBackAtEndOfFrame(VoidCallBack callBack, MonoBehaviour callingObject)
        {
            IEnumerator callBackAtEndOfFrame = CoCallBackAtEndOfFrame(callBack);
            callingObject.StartCoroutine(callBackAtEndOfFrame);
        }

        public static IEnumerator CoCallBackAtEndOfFrame(VoidCallBack callBack)
        {
            yield return new WaitForEndOfFrame();
            callBack?.Invoke();
        }

        

        // NOTE TO SELF: This coroutine does not update in play mode while in scene mode.
        public static void EndOfFrameResizeElementByChildrenSizeDelta(MonoBehaviour callingObject)
        {
            var gameObject = callingObject.gameObject;
            CallBackWithGameObject callBack = ResizeElementByChildrenSizeDelta;
            IEnumerator callBackAtEndOfFrame = CoCallBackAtEndOfFrame(callBack, gameObject);
            callingObject.StartCoroutine(callBackAtEndOfFrame);
        }

        public static void ResizeElementByChildrenSizeDelta(GameObject gameObject)
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(gameObject);
        }

        public static IEnumerator CoCallBackAtEndOfFrame(CallBackWithGameObject callBack, GameObject gameObject)
        {
            yield return new WaitForEndOfFrame();
            callBack?.Invoke(gameObject);
        }


        
    }
}
