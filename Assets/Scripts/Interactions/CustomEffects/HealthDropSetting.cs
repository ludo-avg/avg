using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Interactions.CustomEffects
{
    public class HealthDropSetting : MonoBehaviour
    {
        public AudioClip clip = null;

        private void Awake()
        {
            if (clip == null)
            {
                throw new System.Exception("clip == null");
            }
            HealthDrop.SetAudioClip(clip);
        }
    }
}
