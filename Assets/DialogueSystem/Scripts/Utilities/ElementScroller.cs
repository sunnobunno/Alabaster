using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Alabaster.DialogueSystem.Utilities
{
    public static class ElementScroller
    {

        public static void SlideInElementOffScreen(GameObject gameObject, CallBacks.VoidCallBack slideInEndCallBack, MonoBehaviour callingObject)
        {

            IEnumerator coEaseInElement = EaseInElement(gameObject, slideInEndCallBack, callingObject);
            callingObject.StartCoroutine(coEaseInElement);
            //EaseInElement(gameObject, slideInEndCallBack, callingObject);
        }

        public static void SetElementOffScreen(GameObject gameObject)
        {
            
            
            float screenHeight = DialogueUIController.Instance.ScreenHeight;
            Vector3 elementLocalPosition = gameObject.GetComponent<RectTransform>().localPosition;
            //Vector3 offScreenPosition = new(elementLocalPosition.x, elementLocalPosition.y - screenHeight);
            var timeLinePosition = DialogueMainTimelineContainer.Instance.GetComponent<RectTransform>().localPosition;
            var offScreenOffset = 0f;

            var offScreenPosition = new Vector3(elementLocalPosition.x, -(timeLinePosition.y + offScreenOffset));

            gameObject.GetComponent<RectTransform>().localPosition = offScreenPosition;
            //Debug.Log($"{gameObject.name}: Set off screen");
        }

        public static IEnumerator EaseInElement(GameObject gameObject, CallBacks.VoidCallBack slideInEndCallBack, MonoBehaviour callingObject)
        {
            gameObject.transform.parent.GetComponent<BoxContainer>().Hide();

            yield return new WaitForEndOfFrame();
            SetElementOffScreen(gameObject);

            gameObject.transform.parent.GetComponent<BoxContainer>().Show();

            //callingObject.enabled = true;
            //Debug.Log($"{gameObject.name}: enabled");

            IEnumerator easeInChildElementCoroutine = ParabolicMoveObjectRelative(gameObject,
                DialogueMainTimelineContainer.Instance.EaseInSpeed,
                gameObject.GetComponent<RectTransform>().localPosition,
                new Vector2(0f, 0f),
                slideInEndCallBack);
            callingObject.StartCoroutine(easeInChildElementCoroutine);
        }

        public static void EaseElementToTargetPosition(GameObject objectToMove, Vector3 targetPosition, CallBacks.VoidCallBack callBack, MonoBehaviour callingObject)
        {
            Vector3 initialPosition = objectToMove.GetComponent<RectTransform>().localPosition;

            IEnumerator coParabolicMoveObjectRelative = ParabolicMoveObjectRelative(objectToMove, DialogueMainTimelineContainer.Instance.AutoScrollSpeed, initialPosition, targetPosition, callBack);
            callingObject.StartCoroutine(coParabolicMoveObjectRelative);
        }

        public static IEnumerator ParabolicMoveObjectRelative(GameObject objectToMove, float durationInSeconds, Vector3 initialPosition, Vector3 targetPosition, CallBacks.VoidCallBack callBack)
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

        private static float ParabolicEase(float x, float delta)
        {
            float y = (-1f * delta * x * x) + (2f * delta * x);

            //float y = (-2f * delta) * (x + 1);

            return y;
        }
    }

}
