using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem.Utilities;
using Articy.Little_Guy_Syndrome;
using Articy.Little_Guy_Syndrome.GlobalVariables;
using Articy.Unity;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class SkillCheckInfoController : DialogueElement, IDialogueElementController<IFlowObject>
    {
        [SerializeField] private SimpleTextBoxController skillNameUI;
        [SerializeField] private SimpleTextBoxController skillLevelUI;
        [SerializeField] private DiceController diceController;

        [Header("Testing")]
        [SerializeField] public ArticyRef ArticyRef;


        private GameObject diceControllerObject;

        private SkillEnum skillName;
        private int skillLevel;
        private string odds;
        private string oddsDescription;
        


        public void InitializeElement(IFlowObject aObject)
        {
            skillName = ArticyConversions.GetSkillEnum(aObject);
            skillLevel = ArticyConversions.GetSkillLevel(aObject);
            
            Debug.Log($"Skill {skillName}: {skillLevel}");

            skillNameUI.InitializeElement(skillName.ToString());
            skillLevelUI.InitializeElement(skillLevel.ToString());
            ResizeElement();
        }

        protected override void SetReferences()
        {
            diceControllerObject = diceController.gameObject;
        }

        protected override void SetFields()
        {
            
        }

        public override void GreyOut(bool isGrey)
        {
            
        }

        public override void ResizeElement()
        {
            SetResizedTrue(gameObject);
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

            var selection = Selection.activeGameObject;
            var controller = selection.GetComponent<SkillCheckInfoController>();

            if (GUILayout.Button("Roll Dice"))
            {
                controller.RollDice();
            }
            if (GUILayout.Button("Initialize"))
            {
                controller.InitializeElement(controller.ArticyRef.GetObject());
            }
        }
    }
}

