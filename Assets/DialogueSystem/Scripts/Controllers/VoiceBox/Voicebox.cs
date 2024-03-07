using Alabaster.DialogueSystem;
using Alabaster.DialogueSystem.Controllers;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class Voicebox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private VoicePalette voicePalette;

        private Dictionary<int, char> phonemeScript;

        private void OnEnable()
        {
            //Typewriter.SendCurrentTypewriterIndex += ListenToTypewriter;
            //Typewriter.SendTypewriterStartSignal += InitializeVoicebox;
        }

        private void OnDisable()
        {
            //Typewriter.SendCurrentTypewriterIndex -= ListenToTypewriter;
            //Typewriter.SendTypewriterStartSignal -= InitializeVoicebox;
        }

        public void InitializeVoicebox()
        {
            Debug.Log(textMesh.GetParsedText());
            phonemeScript = GeneratePhonemeScript(textMesh.GetParsedText());
        }

        public void ListenToTypewriter(int index)
        {
            if (phonemeScript.ContainsKey(index))
            {
                SpeakPhoneme(index);
            }
        }

        private void SpeakPhoneme(int index)
        {
            var phoneme = Char.ToLower(phonemeScript[index]);
            Debug.Log(phoneme);

            var audioClip = GetAudioPhoneme(phoneme);

            VoiceSystem.Instance.PlayPhenome(audioClip);
        }


        private AudioClip GetAudioPhoneme(char phoneme)
        {
            switch (phoneme)
            {
                case 'a':
                    return voicePalette.a;
                case 'b':
                    return voicePalette.b;
                case 'c':
                    return voicePalette.c;
                case 'd':
                    return voicePalette.d;
                case 'e':
                    return voicePalette.e;
                case 'f':
                    return voicePalette.f;
                case 'g':
                    return voicePalette.g;
                case 'h':
                    return voicePalette.h;
                case 'i':
                    return voicePalette.i;
                case 'j':
                    return voicePalette.j;
                case 'k':
                    return voicePalette.k;
                case 'l':
                    return voicePalette.l;
                case 'm':
                    return voicePalette.m;
                case 'n':
                    return voicePalette.n;
                case 'o':
                    return voicePalette.o;
                case 'p':
                    return voicePalette.p;
                case 'q':
                    return voicePalette.q;
                case 'r':
                    return voicePalette.r;
                case 's':
                    return voicePalette.s;
                case 't':
                    return voicePalette.t;
                case 'u':
                    return voicePalette.u;
                case 'v':
                    return voicePalette.v;
                case 'w':
                    return voicePalette.w;
                case 'x':
                    return voicePalette.x;
                case 'y':
                    return voicePalette.y;
                case 'z':
                    return voicePalette.z;
                default: return null;
            }
        }

        private Dictionary<int, char> GeneratePhonemeScript(string text)
        {
            var textLength = text.Length;

            var phonemeScript = new Dictionary<int, char>();
            phonemeScript.Add(0, text[0]);

            bool flagNextChar = false;

            for (int i = 0; i < textLength; i++)
            {
                if (flagNextChar)
                {
                    phonemeScript.Add(i, text[i]);
                    flagNextChar = false;
                }

                else if (text[i] == ' ')
                {
                    flagNextChar = true;
                }


            }

            //foreach (var pair in phonemeScript)
            //{
            //    Debug.Log($"{pair.Key}: {pair.Value}");
            //}

            return phonemeScript;
        }
    }


}


