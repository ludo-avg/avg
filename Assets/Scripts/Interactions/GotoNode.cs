using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class GoToNode : InteractionBase
    {
        public InteractionBase node;
        public override void AStart()
        {
            base.AStart();
            base.AEnd();
            InteractionManager.singleton.GotoNode(node);
        }
    }
}
