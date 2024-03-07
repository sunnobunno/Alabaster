using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Voicebox))]
public class VoiceboxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var selection = Selection.activeGameObject.GetComponent<Voicebox>();

        if (GUILayout.Button("Initialize Voicebox"))
        {
            selection.InitializeVoicebox();
        }
    }
}