using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem.Utilities;
using Articy.Little_Guy_Syndrome;
using Articy.Unity;
using System;
using System.Collections;
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
        private SkillCheckInfoState state = SkillCheckInfoState.Hidden;

        private void OnEnable()
        {
            ChoiceBoxController.SendHoverSignal += PeekState;
            ChoiceBoxController.SendExitSignal += HiddenState;
            ChoiceBoxController.SendClickedSignal += RollState;
        }

        private void OnDisable()
        {
            ChoiceBoxController.SendHoverSignal -= PeekState;
            ChoiceBoxController.SendExitSignal -= HiddenState;
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

        private IEnumerator CoResetElement()
        {
            yield return new WaitForSeconds(5f);

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

