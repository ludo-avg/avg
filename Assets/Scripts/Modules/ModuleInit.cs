using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules
{
    public class ModuleInit : MonoBehaviour
    {
        [SerializeField] Background background = null;
        [SerializeField] BackgroundToChange backgroundToChange = null;
        [SerializeField] DialogueBox dialogueBox = null;

        private void Awake()
        {
            background.InitByModuleInit();
            backgroundToChange.InitByModuleInit();
            dialogueBox.InitByModuleInit();

            background.gameObject.SetActive(true);
            backgroundToChange.gameObject.SetActive(false);
        }
    }
}
