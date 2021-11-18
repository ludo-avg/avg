using Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class Idle : InteractionBase
    {
        //setting
        public float time { get { return _time; } private set { _time = value; } }
        [SerializeField] private float _time;
        //runtime
        public float startTime{ get; private set; }
        
        public void DataInit(float time)
        {
            this.time = time;
        }

        public override void AStart()
        {
            base.AStart();
            startTime = Time.time;
            DialogueBox.singleton.Show(false);
        }

        public override void AUpdate()
        {
            base.AUpdate();
            if (Time.time - startTime > time) AEnd();
        }
    }
}
