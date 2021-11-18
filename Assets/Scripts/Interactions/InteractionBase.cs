using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{ 
    public class InteractionBase : MonoBehaviour
    {
        #region TypeDefine

        [Serializable]
        public class ConditionalGoto
        {
            public UnityEvent condition;
            public InteractionBase interaction;

            public ConditionalGoto()
            {
                condition = null;
                interaction = null;
            }
        }
        #endregion

        public bool ended { protected set; get; } = false;

        public ConditionalGoto[] @goto = 
            new ConditionalGoto[1] 
            { 
                new ConditionalGoto { condition = null, interaction = null} 
            };

        public virtual void AStart()
        {
            ended = false;
        }

        public virtual void AEnd()
        {
            ended = true;
        }

        public virtual void AInteract()
        {
            
        }

        public virtual void AUpdate()
        {

        }

    }
}
