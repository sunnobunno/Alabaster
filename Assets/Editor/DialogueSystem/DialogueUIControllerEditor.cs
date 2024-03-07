using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DialogueUIController))]
public class DialogueUIControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {


        DrawDefaultInspector();

        if (GUILayout.Button("Add Dialogue Box"))
        {
            Selection.activeGameObject.GetComponent<DialogueUIController>().CreateDialogueEntry(DialogueLogicController.Instance.FlowPlayer.CurrentObject.GetObject());
        }
    }
}