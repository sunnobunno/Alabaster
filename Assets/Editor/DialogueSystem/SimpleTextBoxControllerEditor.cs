using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleTextBoxController))]
public class SimpleTextBoxControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var selection = Selection.activeGameObject.GetComponent<ContentBoxController>();

        if (GUILayout.Button("Resize Box"))
        {
            selection.ResizeElement();
        }
    }
}