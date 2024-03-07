using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ContentBoxController))]
public class ContentBoxControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Box"))
        {
            Selection.activeGameObject.GetComponent<ContentBoxController>().ResizeElement();
        }
    }
}