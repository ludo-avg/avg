using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.CustomSubs
{
    public class Viewable : Base
    {
        //Setting
        public AudioClip audioClip = null;
        //cached
        GameObject viewThing;
        //runtime
        Coroutine viewCoroutine;

        public override void EnterScenario()
        {
            viewThing = transform.Find("ViewThing").gameObject;
            viewThing.SetActive(false);
        }

        public override void LeaveScenario()
        {
            CompelToClose();
        }

        public override void OnClick()
        {
            CompelToCloseAllViewable();
            viewCoroutine =  StartCoroutine(View());
        }

        IEnumerator View()
        {
            viewThing.SetActive(true);
            Modules.Sfx.singleton.PlayOneShot(audioClip);
            yield return new WaitForSeconds(2);
            viewThing.SetActive(false);
            viewCoroutine = null;
        }

        public void CompelToClose()
        {
            if (viewCoroutine != null)
            {
                StopCoroutine(viewCoroutine);
                viewThing.SetActive(false);
                viewCoroutine = null;
            }
        }

        public void CompelToCloseAllViewable()
        {
            var allSubs = transform.parent.GetComponent<Scenario>().subs;
            foreach (Base sub in allSubs)
            {
                (sub as Viewable)?.CompelToClose();
            }
        }
    }
}

