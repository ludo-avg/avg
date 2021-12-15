using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules
{

    public class Sfx : MonoBehaviour
    {
        #region Type
        public enum State
        {
            Playing,
            Stopped
        }
        #endregion

        #region Singleton
        public static Sfx singleton;
        private void Awake()
        {
            singleton = this;
        }
        #endregion


        //setting
        public AudioClip pickUp = null;
        //cached
        AudioSource audioSource;
        
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }


        public void Play(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        public void PlayOneShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
        public void PlayPickUpSound()
        {
            if (pickUp == null)
            {
                throw new System.Exception("PickUp sound effect is null.");
            }
            audioSource.PlayOneShot(pickUp);
        }


        public void StopVoice()
        {
            audioSource.Stop();
        }

        public State GetState()
        {
            if (audioSource.isPlaying) return State.Playing;
            else return State.Stopped;
        }

    }

}
