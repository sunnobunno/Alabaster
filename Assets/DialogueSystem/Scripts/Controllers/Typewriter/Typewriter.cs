using Alabaster.DialogueSystem.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class Typewriter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;

        private int maxVisibleCharacters;
        private int totalCharacters;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartTypewriter()
        {
            totalCharacters = textMesh.maxVisibleCharacters;
            maxVisibleCharacters = 0;
            var coTypewriter = CoTypewriter();
            StartCoroutine(coTypewriter);
        }

        private IEnumerator CoTypewriter()
        {
            while (maxVisibleCharacters < totalCharacters)
            {
                maxVisibleCharacters++;
                textMesh.maxVisibleCharacters = maxVisibleCharacters;

                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    [CustomEditor(typeof(Typewriter))]
    public class TypewriterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var selection = Selection.activeGameObject.GetComponent<Typewriter>();

            if (GUILayout.Button("Start typewriter"))
            {
                selection.StartTypewriter();
            }
        }
    }
}


