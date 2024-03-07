using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public class VoiceSystem : MonoBehaviour
    {
        public static VoiceSystem Instance;

        private AudioSource audioSource;

        public AudioSource Audio
        {
            get { return audioSource; }
        }

        private void Awake()
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayPhenome(AudioClip clip)
        {
            //audioSource.clip = clip;
            //audioSource.Play();

            audioSource.PlayOneShot(clip);
        }
    }
}


