using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DialogueBoxController))]
public class DialogueBoxControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Box"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().ResizeElement();
        }
        if (GUILayout.Button("Initialize Box"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().InitializeElement(Selection.activeGameObject.GetComponent<DialogueBoxController>().TestArticyRef.GetObject());
        }
        if (GUILayout.Button("Toggle Title Off"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().ToggleTitle(false);
        }
        if (GUILayout.Button("Toggle Title On"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().ToggleTitle(true);
        }
        if (GUILayout.Button("Grey Out Element"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().GreyOut(true);
        }
        if (GUILayout.Button("White Out Element"))
        {
            Selection.activeGameObject.GetComponent<DialogueBoxController>().GreyOut(false);
        }
    }
}
