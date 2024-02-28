using Alabaster.DialogueSystem.Controllers;
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

        public static DialogueElement? GetChildDialogueElementController(GameObject gameObject)
        {
            //Debug.Log("Finding child");
            DialogueElement childDialogueElementController;

            if (gameObject.transform.childCount == 0)
            {
                Debug.LogWarning($"{gameObject.name}: No children with DialogueElement controller");
                return null;
            }

            GameObject child = gameObject.transform.GetChild(0).gameObject;
            if (!child.TryGetComponent<DialogueElement>(out childDialogueElementController))
            {
                return GetChildDialogueElementController(child);
            }

            return childDialogueElementController;
        }

        public static void CallBackAfterChildResize(DialogueElement child, MonoBehaviour callingObject, CallBacks.VoidCallBack callBack)
        {
            IEnumerator coCallBackAfterChildResize = CoCallBackAfterChildResize(child, callBack);
            callingObject.StartCoroutine(coCallBackAfterChildResize);
        }

        private static IEnumerator CoCallBackAfterChildResize(DialogueElement child, CallBacks.VoidCallBack callBack)
        {
            child.ResizeElement();

            while (child.IsResized == false)
            {
                yield return null;
            }

            callBack?.Invoke();
        }
    }
}
