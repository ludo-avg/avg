using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules
{
    public class BackgroundToChange : MonoBehaviour
    {
        static public BackgroundToChange singleton;
        [NonSerialized] public SpriteRenderer sr;

        public void InitByModuleInit()
        {
            singleton = this;
            sr = GetComponent<SpriteRenderer>();
        }
        

    }
}
