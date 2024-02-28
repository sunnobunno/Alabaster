using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class SkillCheckInfoController : DialogueElement
    {
        [SerializeField] private DiceController diceController;
        
        protected override void SetReferences()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetFields()
        {
            throw new System.NotImplementedException();
        }

        public override void GreyOut(bool isGrey)
        {
            throw new System.NotImplementedException();
        }

        public override void ResizeElement()
        {
            throw new System.NotImplementedException();
        }

        public void RollDice()
        {
            diceController.RollDice();
        }
    }

    [CustomEditor(typeof(SkillCheckInfoController))]
    public class SkillCheckInfoControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Roll Dice"))
            {
                Selection.activeGameObject.GetComponent<SkillCheckInfoController>().RollDice();
            }
        }
    }
}

