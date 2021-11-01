using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class CustomInteraction : MonoBehaviour
    {
        //setting
        public CustomInteractions.Base[] subInteractions;

        //runtime
        [NonSerialized] public bool ended = false;
        GameObject downHit;
        

        public void InteractionStart()
        {
            InteractionManager.singleton.DialogueBoxShowOrNot(false);
            gameObject.SetActive(true);
            ended = false;
        }

        public void InteractionEnd()
        {
            foreach (var i in subInteractions)
            {
                i.CustomInteractionEnd();
            }
            gameObject.SetActive(false);
            //ended = true;
        }

        public void InteractionInteract()
        {
            if (Input.GetMouseButtonDown(0))
            {
                downHit = GetMouseHit();

                if (DialogueTypeWriter.singleton.state == DialogueTypeWriter.TypewriterState.Interrupted
                    && downHit == null)
                {
                    DialogueTypeWriter.singleton.CompleteOutput();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                var upHit = GetMouseHit();
                if (upHit != null && upHit == downHit)
                {
                    CustomInteractions.Base b = upHit.GetComponent<CustomInteractions.Base>();
                    b.OnClick();
                }
            }

            if (Input.GetMouseButton(0))
            {
                var currentHit = GetMouseHit();
                if (currentHit != null && currentHit == downHit)
                {
                    
                }
            }
        }

        private GameObject GetMouseHit()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform != null)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    return hitObject;
                }
                else
                    return null;
            }
            return null;
        }
    }
}
