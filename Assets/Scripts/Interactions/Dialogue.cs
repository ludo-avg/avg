using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactions
{
    public class Dialogue
    {
        public string text;
        public string characterName;
        public Dialogue (string text, string characterName = null)
        {
            this.text = text;
            this.characterName = characterName;
        }
    }
}
