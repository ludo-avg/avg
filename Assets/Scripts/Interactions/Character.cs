using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class Character
    {
        public GameObject character { get; private set; }
        public bool show { get; private set; }
        public Character(GameObject character, bool show)
        {
            this.character = character;
            this.show = show;
        }
    }
}
