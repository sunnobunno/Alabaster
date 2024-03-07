using Alabaster.DialogueSystem.Controllers;
using Alabaster.GameState;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    
    
    public class Typewriter : MonoBehaviour
    {
        public static Action<int> SendCurrentTypewriterIndex;
        public static Action SendTypewriterEndSignal;
        public static Action SendTypewriterStartSignal;
        
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Voicebox voicebox;

        private int maxVisibleCharacters;
        private int totalCharacters;
        private TMPro.TMP_TextInfo textInfo;
        private char currentChar;

        private char spaceChar = ' ';
        private List<char> firstLetters;

        private Dictionary<int, char> firstLetterIndexPairing;

        private bool isActive = false;

        private void Awake()
        {
            firstLetters = new List<char>();
            firstLetterIndexPairing = new Dictionary<int, char>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //StoreFirstLetterOfWord();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartTypewriter()
        {
            if (!GameStateController.TypewriterEnabled) { return; }
            if (isActive) { return; }



            Debug.Log("Starting typewriter");

            //StoreFirstLetterOfWord();
            //textInfo = textMesh.textInfo;
            totalCharacters = textMesh.GetParsedText().Length;
            maxVisibleCharacters = 1;
            var coTypewriter = CoTypewriter();
            StartCoroutine(coTypewriter);
        }

        private IEnumerator CoTypewriter()
        {
            voicebox.InitializeVoicebox();
            
            isActive = true;

            var text = textMesh.GetParsedText();

            while (maxVisibleCharacters < totalCharacters)
            {
                textMesh.maxVisibleCharacters = maxVisibleCharacters;
                var currentChar = text[maxVisibleCharacters - 1];
                voicebox.ListenToTypewriter(maxVisibleCharacters - 1);

                if (currentChar == '.' || currentChar == '?' || currentChar == '!')
                {
                    yield return new WaitForSeconds(0.5f);
                }
                else if (currentChar == ',')
                {
                    yield return new WaitForSeconds(0.25f);
                }
                else
                {
                    yield return new WaitForSeconds(0.04f);
                }

                maxVisibleCharacters++;
            }

            textMesh.maxVisibleCharacters = totalCharacters;

            isActive = false;

            SendTypewriterEndSignal?.Invoke();
        }

        private void StoreFirstLetterOfWord()
        {
            var text = textMesh.GetParsedText();
            Debug.Log(text);

            var textLength = text.Length;

            firstLetterIndexPairing.Add(0, text[0]);
            firstLetters.Add(text[0]);

            int lastSpaceIndex = 0;
            bool flagNextChar = false;

            for (int i = 0; i < textLength; i++)
            {
                if (flagNextChar)
                {
                    firstLetterIndexPairing.Add(i, text[i]);
                    firstLetters.Add(text[i]);
                    flagNextChar = false;
                }
                
                else if (text[i] == ' ')
                {
                    flagNextChar = true;
                }


            }

            foreach (var pair in firstLetterIndexPairing)
            {
                Debug.Log($"{pair.Key}: {pair.Value}");
            }

            //Debug.Log(firstLetters.ToString());
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


