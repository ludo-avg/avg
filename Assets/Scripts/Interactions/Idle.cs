using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class Idle
    {
        public float time { get; private set; }
        public float startTime{ get; private set; }
        
        public Idle(float time)
        {
            this.time = time;
        }

        public void InteractionStart()
        {
            startTime = Time.time;
        }
    }
}
