using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactions;

namespace CustomInteractions
{
    public class Paper : Base
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
            CompelToCloseAllNotes();
            noteCoroutine =  StartCoroutine(Note());
        }

        IEnumerator Note()
        {
            note.SetActive(true);
            yield return new WaitForSeconds(2);
            note.SetActive(false);
            noteCoroutine = null;
        }

        public void CompelToCloseNote()
        {
            if (noteCoroutine != null)
            {
                StopCoroutine(noteCoroutine);
                noteCoroutine = null;
                note.SetActive(false);
            }
        }

        public void CompelToCloseAllNotes()
        {
            var sub = transform.parent.gameObject.GetComponent<CustomInteraction>().subInteractions;
            foreach (var item in sub)
            {
                if (item is CustomInteractions.Paper)
                {
                    (item as Paper).CompelToCloseNote();
                }
            }
        }
    }
}

