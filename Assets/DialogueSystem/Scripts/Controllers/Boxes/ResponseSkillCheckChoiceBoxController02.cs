using Articy.Unity;
using Articy.Unity.Interfaces;
using Assets.Dialogue_System.Controllers;
using Assets.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResponseSkillCheckChoiceBoxController02 : ResponseChoiceBoxController02
{



    public override void OnPointerEnter(PointerEventData eventData)
    {
        contentTextMesh.color = Color.white;
        gameObject.GetComponent<Image>().color = Color.black;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        contentTextMesh.color = Color.gray;
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        InvokeSendClickedSignal();
    }

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