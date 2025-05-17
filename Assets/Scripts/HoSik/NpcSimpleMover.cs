using System;
using UnityEngine;

namespace HoSik
{
    public class NpcSimpleMover : SimpleMover
    {
        public AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        public void PlayAudio()
        {
            if (audioSource.isPlaying)
            {
                return;
            }
            else
            {
                audioSource.Play();
            }
        }
        
        void Update()
        {
            MoveAttribute();
        }
    }
}
