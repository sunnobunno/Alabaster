using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillCheckInfoController))]
public class SkillCheckInfoControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var selection = Selection.activeGameObject;
        var controller = selection.GetComponent<SkillCheckInfoController>();

        if (GUILayout.Button("Roll Dice"))
        {
            controller.RollDice();
        }
        if (GUILayout.Button("Initialize"))
        {
            controller.InitializeElement(controller.ArticyRef.GetObject());
        }
    }
}