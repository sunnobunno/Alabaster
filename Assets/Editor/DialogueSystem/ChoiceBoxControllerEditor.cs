using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChoiceBoxController))]
public class ChoiceBoxControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var selection = Selection.activeGameObject.GetComponent<ChoiceBoxController>();

        if (GUILayout.Button("Resize Box"))
        {
            selection.ResizeElement();
        }
        if (GUILayout.Button("Initialize"))
        {
            selection.InitializeElement(selection.TestArticyRef.GetObject());
        }
    }
}
