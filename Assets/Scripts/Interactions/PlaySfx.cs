using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class PlaySfx : InteractionBase
    {
        //Setting 
        public AudioClip clip;

        public override void AStart()
        {
            base.AStart();
            Modules.Sfx.singleton.PlayOneShot(clip);
            AEnd();
        }
    }
}
