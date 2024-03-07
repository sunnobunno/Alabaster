using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DialogueLogicController))]
public class DialogueLogicControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Start Dialogue"))
        {
            Selection.activeGameObject.GetComponent<DialogueLogicController>().StartDialogue(DialogueLogicController.Instance.TestArticyRef);
        }
    }
}
