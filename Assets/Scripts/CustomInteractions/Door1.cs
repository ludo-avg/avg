using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactions;

namespace CustomInteractions
{
    public class Door1 : Base
    {
        //Setting
        public CustomInteraction doorTo;

        public override void CustomInteractionStart()
        {
            
        }

        public override void CustomInteractionEnd()
        {
            
        }

        public override void OnClick()
        {
            transform.parent.gameObject.SetActive(false);
            transform.parent.gameObject.GetComponent<CustomInteraction>().InteractionEnd();

            doorTo.gameObject.SetActive(true);
        }
    }
}

