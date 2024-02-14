using Articy.Unity;
using Articy.Unity.Interfaces;
using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Alabaster.DialogueSystem.Controllers
{
    public class ResponseSkillCheckChoiceBoxController02 : ChoiceBoxController
    {



        //public override void OnPointerEnter(PointerEventData eventData)
        //{
        //    contentObjectController.TextColor = Color.white;
        //    gameObject.GetComponent<Image>().color = Color.black;
        //}

        //public override void OnPointerExit(PointerEventData eventData)
        //{
        //    contentObjectController.TextColor = Color.gray;
        //    gameObject.GetComponent<Image>().color = Color.white;
        //}

        //public override void OnPointerClick(PointerEventData eventData)
        //{
        //    SendClickedSignal?.Invoke(branch);
        //    //InvokeSendClickedSignal();
        //}

    }

    [CustomEditor(typeof(ResponseSkillCheckChoiceBoxController02))]
    public class ResponseSkillCheckChoiceBoxController02Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Resize Box"))
            {
                Selection.activeGameObject.GetComponent<ResponseSkillCheckChoiceBoxController02>().ResizeElement();
            }
        }
    }
}

