using Alabaster.DialogueSystem;
using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem.Utilities;
using Articy.Unity;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class DiceController : DialogueElement
    {
        public static Action SendDiceAnimationEndSignal;
        
        [SerializeField] private GameObject dice1;

        [SerializeField] private SimpleTextBoxController dice1Value;
        [SerializeField] private SimpleTextBoxController dice2Value;
        [SerializeField] private Animator animator;
        

        protected override void Start()
        {
            base.Start();
        }

        private void OnEnable()
        {
            SkillCheckInfoController.SendResetSignal += ResetDice;
            Die.SendDiceAnimationEndSignal += InvokeDiceAnimationEndSignal;
        }

        private void OnDisable()
        {
            SkillCheckInfoController.SendResetSignal -= ResetDice;
            Die.SendDiceAnimationEndSignal -= InvokeDiceAnimationEndSignal;
        }

        protected override void SetReferences()
        {

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

        public void RollDice(int[] diceValues)
        {
            dice1Value.Content = diceValues[0].ToString();
            dice2Value.Content = diceValues[1].ToString();

            animator.SetBool("Rolling", true);
        }

        private void InvokeDiceAnimationEndSignal()
        {
            SendDiceAnimationEndSignal?.Invoke();
        }

        public void ResetDice()
        {
            animator.SetBool("Rolling", false);
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
                Selection.activeGameObject.GetComponent<DiceController>().RollDice(DiceRoller.Roll2D6());
            }
            if (GUILayout.Button("Reset Dice"))
            {
                Selection.activeGameObject.GetComponent<DiceController>().ResetDice();
            }

        }
    }
}


