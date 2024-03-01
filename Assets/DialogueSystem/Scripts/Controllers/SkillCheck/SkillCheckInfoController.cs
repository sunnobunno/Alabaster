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
        public static SkillCheckInfoController Instance { get; private set; }
        
        [SerializeField] private SimpleTextBoxController skillNameUI;
        [SerializeField] private SimpleTextBoxController skillLevelUI;
        [SerializeField] private DiceController diceController;
        [SerializeField] private Animator animator;

        [Header("Testing")]
        [SerializeField] public ArticyRef ArticyRef;


        private GameObject diceControllerObject;

        private IFlowObject aObject;
        private SkillEnum skillName;
        private int skillLevel;
        private string odds;
        private string oddsDescription;

        private void OnEnable()
        {
            ChoiceBoxController.SendHoverSignal += PeekView;
            ChoiceBoxController.SendExitSignal += HiddenView;
        }

        private void OnDisable()
        {
            ChoiceBoxController.SendHoverSignal -= PeekView;
            ChoiceBoxController.SendExitSignal -= HiddenView;
        }

        protected override void Awake()
        {
            base.Awake();

            if (Instance == null )
            {
                Instance = this;
            }
        }

        public void InitializeElement(IFlowObject aObject)
        {
            SetContent(aObject);
            ResizeElement();
        }

        protected override void SetReferences()
        {
            diceControllerObject = diceController.gameObject;
        }

        protected override void SetFields()
        {
            
        }

        public void SetContent(IFlowObject aObject)
        {
            this.aObject = aObject;
            skillName = ArticyConversions.GetSkillEnum(aObject);
            skillLevel = ArticyConversions.GetSkillLevel(aObject);

            Debug.Log($"Skill {skillName}: {skillLevel}");

            skillNameUI.InitializeElement(skillName.ToString());
            skillLevelUI.InitializeElement(skillLevel.ToString());
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
            var diceValues = DiceRoller.Roll2D6();
            
            diceController.RollDice(diceValues);
        }

        private void ResetAnimatorParameters()
        {
            animator.SetBool("Peek", false);
            animator.SetBool("Hidden", false);
        }

        private void PeekView(IFlowObject aObject)
        {
            ResetAnimatorParameters();
            animator.SetBool("Peek", true);
        }

        private void HiddenView()
        {
            ResetAnimatorParameters();
            animator.SetBool("Hidden", true);
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

