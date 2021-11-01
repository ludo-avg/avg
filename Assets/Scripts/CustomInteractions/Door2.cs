using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInteractions
{
    public class Door2 : Base
    {
        //cached
        GameObject note;
        //runtime
        Coroutine noteCoroutine;
        bool firstTime = true;

        public override void CustomInteractionStart()
        {
            note = transform.Find("Note").gameObject;
        }

        public override void CustomInteractionEnd()
        {
            CompelToCloseNote();
        }

        public override void OnClick()
        {
            if (firstTime)
            {
                firstTime = false;
                CustomInteractionStart();
            }
            CompelToCloseNote();
            noteCoroutine =  StartCoroutine(Note());
        }

        IEnumerator Note()
        {
            note.SetActive(true);
            yield return new WaitForSeconds(2);
            note.SetActive(false);
            noteCoroutine = null;
        }

        void CompelToCloseNote()
        {
            if (noteCoroutine != null)
            {
                StopCoroutine(noteCoroutine);
                note.SetActive(false);
            }
            
        }
    }
}

