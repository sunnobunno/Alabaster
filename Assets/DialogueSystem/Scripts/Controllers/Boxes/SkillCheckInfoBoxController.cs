using Articy.Unity;
using Alabaster.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
//using UnityEngine.Rendering.LookDev;

namespace Alabaster.DialogueSystem.Controllers
{
    public class SkillCheckInfoBoxController : MonoBehaviour, IDialogueElementController<IFlowObject>
    {

        public string SkillName { get => skillName; }
        public string ChanceDescription { get => chanceDescription; }
        public int PercentChance { get => percentChance; }
        public List<string> Bonuses { get => bonuses; }

        [Header("Content")]
        [SerializeField] private string skillName;
        [SerializeField] private string chanceDescription;
        [SerializeField] private int percentChance;
        [SerializeField] private List<string> bonuses;

        [Header("Child Objects")]
        [SerializeField] private GameObject backgroundObject;
        [SerializeField] private GameObject skillNameObject;
        [SerializeField] private GameObject chanceDescriptionObject;
        [SerializeField] private GameObject percentChanceObject;
        [SerializeField] private GameObject bonusesObject;

        private RectTransform rectTransform;
        private RectTransform backgroundObjectRectTransform;
        private RectTransform skillNameObjectRectTransform;
        private RectTransform chanceDescriptionObjectRectTransform;
        private RectTransform percentChanceObjectRectTransform;
        private RectTransform bonusesObjectRectTransform;

        private TextMeshProUGUI skillNameObjectTextMesh;
        private TextMeshProUGUI chanceDescriptionObjectTextMesh;
        private TextMeshProUGUI percentChanceObjectTextMesh;
        private TextMeshProUGUI bonusesObjectTextMesh;


        public void InitializeElement(string skillName, List<string> bonuses)
        {
            SetElementComponentReferences();
            SetElementContent(skillName);
            SetElementFont();
            SetElementSizeDelta();
        }

        public void InitializeElement(string content)
        {

        }

        public void InitializeElement(IFlowObject aObject)
        {

        }

        public void SetElementContent(string content)
        {

        }

        public void SetElementContent(IFlowObject aObject)
        {

        }

        protected void SetElementComponentReferences()
        {
            rectTransform = GetComponent<RectTransform>();

            backgroundObjectRectTransform = backgroundObject.GetComponent<RectTransform>();
            skillNameObjectRectTransform = skillNameObject.GetComponent<RectTransform>();
            chanceDescriptionObjectRectTransform = chanceDescriptionObject.GetComponent<RectTransform>();
            percentChanceObjectRectTransform = percentChanceObject.GetComponent<RectTransform>();
            bonusesObjectRectTransform = bonusesObject.GetComponent<RectTransform>();

            skillNameObjectTextMesh = skillNameObject.GetComponent<TextMeshProUGUI>();
            chanceDescriptionObjectTextMesh = chanceDescriptionObject.GetComponent<TextMeshProUGUI>();
            percentChanceObjectTextMesh = percentChanceObject.GetComponent<TextMeshProUGUI>();
            bonusesObjectTextMesh = bonusesObject.GetComponent<TextMeshProUGUI>();
        }

        protected void SetElementFont()
        {

        }

        protected void SetElementSizeDelta()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ResizeElement()
        {
            throw new System.NotImplementedException();
        }

        public void GreyOut(bool isGrey)
        {
            throw new System.NotImplementedException();
        }
    }
}


