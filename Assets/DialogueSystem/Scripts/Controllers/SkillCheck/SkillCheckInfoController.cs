using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem.Utilities;
using Articy.Little_Guy_Syndrome;
using Articy.Unity;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    internal enum SkillCheckInfoState
    {
        Hidden,
        Hover,
        Rolling
    }

    public class SkillCheckInfoController : DialogueElement, IDialogueElementController<IFlowObject>
    {
        public static SkillCheckInfoController Instance { get; private set; }

        public static Action SendResetSignal;
        public static Action<int> SendResultSignal;
        public static Action<bool> SendPassedSignal;

        [SerializeField] private SimpleTextBoxController skillNameUI;
        [SerializeField] private SimpleTextBoxController difficultyLevelUI;
        [SerializeField] private DiceController diceController;
        [SerializeField] private Animator animator;

        [Header("Testing")]
        [SerializeField] public ArticyRef ArticyRef;

        public static bool IsRollFinished { get => isRollFinished; }
        public static bool IsPassed { get => isPassed; }

        private GameObject diceControllerObject;
        private IFlowObject aObject;
        private SkillEnum skillName;
        private int skillLevel;
        private int difficultyLevel;
        private string odds;
        private string oddsDescription;
        private SkillCheckInfoState state = SkillCheckInfoState.Hidden;
        private static bool overrideResetCo = false;
        private int[] diceValues;
        private static bool isPassed;
        private static bool isRollFinished = false;

        private void OnEnable()
        {
            ChoiceBoxController.SendHoverSignal += PeekState;
            ChoiceBoxController.SendExitSignal += HiddenState;
            ChoiceBoxController.SendClickedSignal += RollState;
            DiceController.SendDiceAnimationEndSignal += ListenDiceRollEndSignal;
        }

        private void OnDisable()
        {
            ChoiceBoxController.SendHoverSignal -= PeekState;
            ChoiceBoxController.SendExitSignal -= HiddenState;
            ChoiceBoxController.SendClickedSignal -= RollState;
            DiceController.SendDiceAnimationEndSignal -= ListenDiceRollEndSignal;
        }

        protected override void Awake()
        {
            base.Awake();

            if (Instance == null)
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
            difficultyLevel = ArticyConversions.GetSkillCheckRequirement(aObject);

            Debug.Log($"Skill {skillName}: {skillLevel}");

            skillNameUI.InitializeElement(skillName.ToString());
            difficultyLevelUI.InitializeElement($"DIFFICULTY: {difficultyLevel}");
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
            diceValues = DiceRoller.Roll2D6();
            diceController.RollDice(diceValues);
        }

        private void ListenDiceRollEndSignal()
        {
            var totalDiceValue = diceValues[0] + diceValues[1];
            SendResultSignal?.Invoke(totalDiceValue);
            Debug.Log(totalDiceValue);

            isPassed = false;

            if (totalDiceValue >= difficultyLevel)
            {
                isPassed = true;
            }

            SendPassedSignal?.Invoke(isPassed);
            Debug.Log(isPassed);

            //isRollFinished = true;
        }

        private void ResetAnimatorParameters()
        {
            animator.SetBool("Peek", false);
            animator.SetBool("Hidden", false);
            animator.SetBool("Rolling", false);
            
        }

        private void PeekState(IFlowObject aObject)
        {
            InitializeElement(aObject);
            
            state = SkillCheckInfoState.Hover;
            HandleState();
            SendResetSignal?.Invoke();
        }

        private void HiddenState()
        {
            state = SkillCheckInfoState.Hidden;
            HandleState();
        }

        private void RollState(Branch branch)
        {
            if (!ArticyConversions.GetIsSkillCheck(branch.Target)) return;

            RollDice();
            state = SkillCheckInfoState.Rolling;
            HandleState();

            var coResetElement = CoResetElement();
            StartCoroutine(coResetElement);
        }


        // TODO this override solution is jank. There's gotta be a better way.
        // Perhaps creating some sort of central timer that can be updated to allow the reset signal to be prolonged?
        private bool resetOverrideStage1 = false;
        private bool resetOverrideStage2 = false;
        private IEnumerator CoResetElement()
        {
            if (resetOverrideStage1)
            {
                resetOverrideStage2 = true;
            }
            
            resetOverrideStage1 = true;
            
            yield return new WaitForSeconds(5f);

            if (resetOverrideStage2)
            {
                
                resetOverrideStage2 = false;
                yield break;
            }

            resetOverrideStage1 = false;

            ResetAnimatorParameters();
            SendResetSignal?.Invoke();
        }

        

        private void HandleState()
        {
            ResetAnimatorParameters();

            switch (state)
            {
                case SkillCheckInfoState.Hidden:
                    animator.SetBool("Hidden", true);
                    break;
                case SkillCheckInfoState.Hover:
                    animator.SetBool("Peek", true);
                    break;
                case SkillCheckInfoState.Rolling:
                    animator.SetBool("Rolling", true);
                    break;
                default: break;
            }
        }
    }

    
}

