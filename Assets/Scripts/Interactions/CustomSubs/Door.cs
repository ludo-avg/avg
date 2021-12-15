using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.CustomSubs
{
    public class Door : Base
    {
        //Setting
        public Scenario doorTo;
        public AudioClip audioClip = null;

        public override void EnterScenario()
        {
            
        }

        public override void LeaveScenario()
        {
            
        }

        public override void OnClick()
        {
            transform.parent.gameObject.SetActive(false);
            doorTo.gameObject.SetActive(true);
            if (audioClip != null)
            {
                Modules.Sfx.singleton.PlayOneShot(audioClip);
            }
        }
    }
}

