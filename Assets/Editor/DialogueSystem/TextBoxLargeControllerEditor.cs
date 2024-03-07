using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TextBoxLargeController))]
public class TextBoxLargeControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Box"))
        {
            Selection.activeGameObject.GetComponent<TextBoxLargeController>().ResizeElement();
        }
    }
}