using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Typewriter))]
public class TypewriterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var selection = Selection.activeGameObject.GetComponent<Typewriter>();

        if (GUILayout.Button("Start typewriter"))
        {
            selection.StartTypewriter();
        }
    }
}