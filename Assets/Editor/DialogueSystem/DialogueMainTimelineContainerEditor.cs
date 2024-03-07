using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DialogueMainTimelineContainer))]
public class DialogueMainTimelineContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var selection = Selection.activeGameObject.GetComponent<DialogueMainTimelineContainer>();

        DrawDefaultInspector();

        if (GUILayout.Button("Add Dialogue Box"))
        {
            selection.AddDialogueElement(selection.DialogueBoxPrefab, selection.TestArticyRef.GetObject());
        }

        if (GUILayout.Button("Add Continue Box"))
        {
            selection.AddDialogueElement(selection.ContinueBoxPrefab, selection.TestArticyRef.GetObject());
        }

        if (GUILayout.Button("Add Response List"))
        {
            selection.AddDialogueElement(selection.ResponseListContainerPrefab, selection.TestArticyRef.GetObject());
        }
    }
}
