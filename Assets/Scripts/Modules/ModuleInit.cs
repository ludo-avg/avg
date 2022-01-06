using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules
{
    public class ModuleInit : MonoBehaviour
    {
        [SerializeField] DialogueBox dialogueBox = null;
        [SerializeField] Temp.ScreenFlash screenFlash = null;
        [SerializeField] Temp.ShowDamage showDamage = null;

        private void Awake()
        {
            dialogueBox.InitByModuleInit();
            screenFlash.InitByModuleInit();
            showDamage.InitByModuleInit();
        }
    }
}
