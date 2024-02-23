using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Alabaster.DialogueSystem.Utilities
{
    public static class RectTransformSizeFitter
    {
        public static Vector2 GetSizeOfChildren(GameObject parent)
        {
            

            

            UnityEngine.Transform transform = parent.transform;

            var children = new List<RectTransform>();
            foreach (UnityEngine.Transform child in transform)
            {
                children.Add(child.GetComponent<RectTransform>());
            }
            //RectTransform children = transform.GetComponentInChildren<RectTransform>();

            //Debug.Log($"{parent.name} Child Count: {children.Count}");

            float min_x, max_x, min_y, max_y;
            min_x = max_x = 0f;
            min_y = max_y = 0f;

            int i = 0;

            foreach (RectTransform child in children)
            {
                //Debug.Log($"CHILD {i}");
                i++;

                //Vector2 scale = child.sizeDelta;
                float temp_min_x, temp_max_x, temp_min_y, temp_max_y;
                RectTransform childRectTransform = child.GetComponent<RectTransform>();

                temp_min_x = child.localPosition.x;
                temp_max_x = child.localPosition.x + childRectTransform.sizeDelta.x;
                temp_min_y = Mathf.Abs(child.localPosition.y);
                temp_max_y = Mathf.Abs(child.localPosition.y) + childRectTransform.sizeDelta.y;

                //Debug.Log($"localPosition.y: {child.localPosition.y}");
                //Debug.Log($"temp_min_x: {temp_min_x}");
                //Debug.Log($"temp_max_x: {temp_max_x}");
                //Debug.Log($"temp_min_y: {temp_min_y}");
                //Debug.Log($"temp_max_y: {temp_max_y}");


                if (temp_min_x < min_x)
                    min_x = temp_min_x;
                if (temp_max_x > max_x)
                    max_x = temp_max_x;

                if (temp_min_y < min_y)
                    min_y = temp_min_y;
                if (temp_max_y > max_y)
                    max_y = temp_max_y;
            }


            //Debug.Log("RESULTS");
            //Debug.Log($"min_x: {min_x}");
            //Debug.Log($"max_x: {max_x}");
            //Debug.Log($"min_y: {min_y}");
            //Debug.Log($"max_y: {max_y}");

            

            Vector2 sizeOfChildren = new Vector2(max_x - min_x, max_y - min_y);

            

            return sizeOfChildren;
        }
    }
}


