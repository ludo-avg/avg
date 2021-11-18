using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;


namespace Interactions
{
    public class Dialogue: InteractionBase
    {
        public string text;
        public string characterName;
        public void DataInit (string text, string characterName = null)
        {
            this.text = text;
            this.characterName = characterName;
        }

        public override void AStart()
        {
            base.AStart();

            var writer = DialogueTypeWriter.singleton;
            var box = DialogueBox.singleton;

            box.Show(true);
            if (characterName != null)
            {
                box.SetName(characterName);
            }
            else
            {
                box.SetName("");
            }

            writer.OutputText(text);
        }

        public override void AInteract()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var writer = DialogueTypeWriter.singleton;

                if (writer.state == DialogueTypeWriter.TypewriterState.Outputting)
                {
                    writer.CompleteOutput();
                }
                else if (writer.state == DialogueTypeWriter.TypewriterState.Completed)
                {
                    AEnd();
                }
            }
        }
    }
}
