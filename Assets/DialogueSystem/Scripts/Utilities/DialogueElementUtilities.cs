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
    }
}
