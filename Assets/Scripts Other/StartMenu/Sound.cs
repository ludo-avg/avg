using System;
using UnityEngine;
using DG.Tweening;

namespace StartMenu
{
    public class Sound : MonoBehaviour
    {
        #region Singleton
        public static Sound singleton;
        private void Awake()
        {
            singleton = this;
        }
        #endregion


        [NonSerialized] public AudioSource bgmAS;
        [NonSerialized] public AudioSource sfxAS;
        [NonSerialized] public AudioSource voiceLaoMuGenAS;
        [NonSerialized] public AudioSource voiceAWenAS;

        public AudioClip transitionClip;
        public AudioClip clickClip;
        public AudioClip fartClip;

        void Start()
        {
            bgmAS = transform.Find("BGM").GetComponent<AudioSource>();
            sfxAS = transform.Find("SFX").GetComponent<AudioSource>();
            voiceLaoMuGenAS = transform.Find("VoiceLaoMuGen").GetComponent<AudioSource>();
            voiceAWenAS = transform.Find("VoiceAWen").GetComponent<AudioSource>();

            bgmAS.volume = 0.3f;
            voiceLaoMuGenAS.volume = 1f;
            voiceAWenAS.volume = 1f;
        }

        public void BgmAndVoiceDown(float time = 0.5f)
        {
            DOTween.To(() => bgmAS.volume, value => bgmAS.volume = value, 0, time);
            DOTween.To(() => voiceLaoMuGenAS.volume, value => voiceLaoMuGenAS.volume = value, 0, time);
            DOTween.To(() => voiceAWenAS.volume, value => voiceAWenAS.volume = value, 0, time);
        }


    }
}