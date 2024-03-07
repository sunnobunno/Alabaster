using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ChoiceListContainerController))]
public class ChoiceListContainerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Box"))
        {
            Selection.activeGameObject.GetComponent<ChoiceListContainerController>().ResizeElement();
        }
        if (GUILayout.Button("Initialize Element"))
        {
            Selection.activeGameObject.GetComponent<ChoiceListContainerController>().InitializeElement(Selection.activeGameObject.GetComponent<ChoiceListContainerController>().TestArticyRef.GetObject());
        }
    }
}
