using Alabaster.DialogueSystem;
using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class DiceController : DialogueElement
    {
        [SerializeField] private GameObject dice1;

        [SerializeField] private Animator dice1Animator;
        [SerializeField] private Animator dice2Animator;

        protected override void Start()
        {
            base.Start();

            dice1Animator.Play("Dice01Idle", 0, 0f);
            dice1Animator.Play("Dice01Idle", 0, 0.1f);
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
}


