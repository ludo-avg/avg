using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules
{
    public class Background : MonoBehaviour
    {
        static public Background singleton;

        [NonSerialized] public SpriteRenderer sr;
        public void InitByModuleInit()
        {
            singleton = this;
            sr = GetComponent<SpriteRenderer>();
        }
    }
}