using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class ChangeBgm : InteractionBase
    {
        //Setting 
        public AudioClip clip;
        public float time = 0.75f;

        public override void AStart()
        {
            IEnumerator End()
            {
                yield return new WaitForSeconds(time);
                AEnd();
                gameObject.SetActive(false);
            }

            base.AStart();
            gameObject.SetActive(true);
            Modules.Bgm.singleton.ChangeBgm(clip, time);
            StartCoroutine(End());
        }

    }
}