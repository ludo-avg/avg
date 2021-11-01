using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactions;

public class InteractionInteract : MonoBehaviour
{
    private void Update()
    {
        InteractionManager interaction = InteractionManager.singleton;
        
        if (interaction.current is Idle)
        {
            var idle = interaction.current as Idle;
            if (Time.time - idle.startTime > idle.time)
            {
                interaction.Next();
            }
        }

        else if (interaction.current is Dialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DialogueTypeWriter writer = DialogueTypeWriter.singleton;

                if (writer.state == DialogueTypeWriter.TypewriterState.Outputting)
                {
                    writer.CompleteOutput();
                }
                else if (writer.state == DialogueTypeWriter.TypewriterState.Completed)
                {
                    interaction.Next();
                }
            }
        }

        else if (interaction.current is TimeChoice)
        {
            TimeChoice tc = interaction.current as TimeChoice;

            tc.InteractionInteract();
            
            if (tc.ended)
            {
                interaction.Next();
            }
        }
        else if (interaction.current is Choice)
        {
            Choice choice = interaction.current as Choice;

            choice.InteractionInteract();

            if (choice.ended)
            {
                interaction.Next();
            }
        }
        else if (interaction.current is CustomInteraction)
        {
            CustomInteraction custom = interaction.current as CustomInteraction;

            custom.InteractionInteract();

            if (custom.ended)
            {
                interaction.Next();
            }
        }
    }
}
