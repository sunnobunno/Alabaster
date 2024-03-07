using Alabaster.DialogueSystem.Utilities;
using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DiceController))]
public class DiceControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Roll Dice"))
        {
            Selection.activeGameObject.GetComponent<DiceController>().RollDice(DiceRoller.Roll2D6());
        }
        if (GUILayout.Button("Reset Dice"))
        {
            Selection.activeGameObject.GetComponent<DiceController>().ResetDice();
        }

    }
}
