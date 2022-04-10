using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Modules
{
    public class Bgm : MonoBehaviour
    {
        #region Singleton
        public static Bgm singleton;
        private void Awake()
        {
            singleton = this;
        }
        #endregion
        

        //cached
        private AudioSource audioSource;
        private float volume;
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            volume = audioSource.volume;
        }

        public void ChangeBgm(AudioClip clip, float time)
        {
            void NewBgm()
            {
                audioSource.clip = clip;
                audioSource.Play();
                DOTween.To(
                () => 0,
                value => audioSource.volume = value,
                volume,
                time
                );
            }

            DOTween.To(
                () => volume,
                value => audioSource.volume = value,
                0,
                time
                )
                .OnComplete(NewBgm);
        }

        public void VolumeDown(float time)
        {
            DOTween.To(
                () => volume,
                value => audioSource.volume = value,
                0,
                time
                );
        }
    }
}
