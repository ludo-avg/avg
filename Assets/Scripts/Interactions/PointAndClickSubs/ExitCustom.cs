using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.PointAndClickSubs
{
    public class ExitCustom : Base
    {
        public override void OnClick()
        {
            transform.parent.GetComponent<PointAndClick>().ManuallyExit();
        }
    }
}