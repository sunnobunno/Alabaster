using Alabaster.DialogueSystem.Controllers;
using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxContainer))]
public class BoxContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Container"))
        {
            Selection.activeGameObject.GetComponent<BoxContainer>().ResizeElement();
        }
        if (GUILayout.Button("Slide In Element"))
        {
            Selection.activeGameObject.GetComponent<BoxContainer>().SlideInElement();
        }
        if (GUILayout.Button("Destroy"))
        {
            Selection.activeGameObject.GetComponent<BoxContainer>().DestroySelf();
        }
        if (GUILayout.Button("Initialize"))
        {
            Selection.activeGameObject.GetComponent<BoxContainer>().InitializeElement<IFlowObject>(Selection.activeGameObject.GetComponent<BoxContainer>().testArticyRef.GetObject());
        }
    }
}