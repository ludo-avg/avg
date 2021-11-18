using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.CustomSubs
{
    public class ExitCustom : Base
    {
        public override void OnClick()
        {
            transform.parent.GetComponent<Custom>().ManuallyExit();
        }
    }
}