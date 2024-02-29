using Alabaster.DialogueSystem;
using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem.Utilities;
using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class DiceController : DialogueElement
    {
        [SerializeField] private GameObject dice1;

        [SerializeField] private Animator dice1Animator;
        [SerializeField] private Animator dice2Animator;
        [SerializeField] private SimpleTextBoxController dice1Value;
        [SerializeField] private SimpleTextBoxController dice2Value;

        protected override void Start()
        {
            base.Start();

            dice1Animator.Play("Dice01Idle", 0, 0f);
            dice2Animator.Play("Dice01Idle", 0, 0.1f);
        }

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

        public void RollDice(int[] diceValues)
        {
            dice1Value.Content = diceValues[0].ToString();
            dice2Value.Content = diceValues[1].ToString();

            dice1Animator.SetBool("Rolling", true);
            dice2Animator.SetBool("Rolling", true);
        }

        private void DisableDice1()
        {

        }

        private void EnableDice1()
        {

        }

        public void ResetDice()
        {
            dice1Animator.SetBool("Rolling", false);
            dice2Animator.SetBool("Rolling", false);

            dice1Animator.Play("Dice01Idle", 0, 0f);
            dice2Animator.Play("Dice01Idle", 0, 0.1f);
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


