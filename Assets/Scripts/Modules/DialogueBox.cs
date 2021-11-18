using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Modules
{
    public class DialogueBox : MonoBehaviour
    {
        public static DialogueBox singleton = null;
        private TMP_Text nameTMP;

        public void InitByModuleInit()
        {
            singleton = this;
            nameTMP = transform.Find("Name").Find("Text").GetComponent<TMP_Text>();
        }

        #region Public Method
        public void Show(bool isShow)
        {
            if (isShow)
            {
                if (gameObject.activeSelf == false)
                {
                    gameObject.SetActive(true);
                }
            }
            else
            {
                if (gameObject.activeSelf == true)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void SetName(string name)
        {
            nameTMP.text = name;
        }
        #endregion
    }
}