using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules
{

    public class Voice : MonoBehaviour
    {
        #region Type
        public enum State
        {
            Playing,
            Stopped
        }
        #endregion

        #region Singleton
        public static Voice singleton;
        private void Awake()
        {
            singleton = this;
        }
        #endregion


        AudioSource audioSource;
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }


        public void PlayVoice(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
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

