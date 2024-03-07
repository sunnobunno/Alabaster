using Alabaster.EnvironmentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ParallaxElementUI))]
public class ParallaxElementUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize"))
        {
            Selection.activeGameObject.GetComponent<ParallaxElementUI>().ResizeElement();
        }
    }
}