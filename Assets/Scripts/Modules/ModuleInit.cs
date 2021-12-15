using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules
{
    public class ModuleInit : MonoBehaviour
    {
        [SerializeField] DialogueBox dialogueBox = null;

        private void Awake()
        {
            dialogueBox.InitByModuleInit();
        }
    }
}
