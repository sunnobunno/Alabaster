using Alabaster.DialogueSystem.Utilities;
using Articy.Unity;
using Alabaster.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class DialogueLogicController : MonoBehaviour, IArticyFlowPlayerCallbacks
    {
        public static DialogueLogicController Instance { get; private set; }

        public static event Action DialogueStart;
        public static event Action DialogueEnd;
        
        [SerializeField] private ArticyFlowPlayer flowPlayer;
        [SerializeField] private ArticyRef testArticyRef;

        private bool isPaused;

        public bool IsPaused { get { return isPaused; } }

        public ArticyFlowPlayer FlowPlayer
        {
            get { return flowPlayer; }
        }

        public ArticyRef TestArticyRef { get { return testArticyRef; } }

        public void StartDialogue(ArticyRef articyRef)
        {
            var articyObject = articyRef.GetObject();

            Debug.Log(articyObject.Id);

            flowPlayer.StartOn = articyObject;
            isPaused = false;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            
        }

        private void SetElementFields()
        {
            flowPlayer = GetComponent<ArticyFlowPlayer>();

            isPaused = true;
        }

        public void OnFlowPlayerPaused(IFlowObject aObject)
        {
            Debug.Log(ArticyConversions.IFlowObjectToText(aObject));

            isPaused = true;
        }

        public void OnBranchesUpdated(IList<Branch> aBranches)
        {
            
        }
    }

    [CustomEditor(typeof(DialogueLogicController))]
    public class DialogueLogicController02Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Start Dialogue"))
            {
                Selection.activeGameObject.GetComponent<DialogueLogicController>().StartDialogue(DialogueLogicController.Instance.TestArticyRef); ;
            }
        }
    }

}
