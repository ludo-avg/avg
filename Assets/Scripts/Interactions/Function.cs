using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using UnityEngine.Events;

namespace Interactions
{
    public class Function : InteractionBase
    {
        public UnityEvent function;
        public void DataInit()
        {

        }

        public override void AStart()
        {
            base.AStart();
            function.Invoke();
            AEnd();
        }

    }
}
