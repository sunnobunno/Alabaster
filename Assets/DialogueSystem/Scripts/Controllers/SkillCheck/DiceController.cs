using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DiceController : DialogueElement
{
    [SerializeField] private GameObject dice1;

    private Animator dice1Animator;

    protected override void SetReferences()
    {
        dice1Animator = dice1.GetComponent<Animator>();
    }

    protected override void SetFields()
    {
        
    }

    public override void GreyOut(bool isGrey)
    {

    }

    public override void ResizeElement()
    {

    }

    public void RollDice()
    {

        dice1Animator.ResetTrigger("StartTrigger");
        dice1Animator.SetTrigger("StartTrigger");
    }

    private void DisableDice1()
    {
        
    }

    private void EnableDice1()
    {
        
    }

    public void ResetDice()
    {
        dice1Animator.ResetTrigger("ResetTrigger");
        dice1Animator.SetTrigger("ResetTrigger");
    }
}

[CustomEditor(typeof(DiceController))]
public class DiceControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Roll Dice"))
        {
            Selection.activeGameObject.GetComponent<DiceController>().RollDice();
        }
        if (GUILayout.Button("Reset Dice"))
        { 
            Selection.activeGameObject.GetComponent<DiceController>().ResetDice(); 
        }

    }
}
