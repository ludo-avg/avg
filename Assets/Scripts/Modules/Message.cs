using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools.Extensions;

namespace Modules
{
    public class Message : MonoBehaviour
    {
        #region
        public static Message singleton = null;
        private void Awake()
        {
            singleton = this;
        }
        #endregion

        GameObject messageBox;
        TMP_Text textMeshPro;
        Coroutine showCoroutine = null;

        void Start()
        {
            messageBox = transform.LudoFind("MessageBox", includeInactive: true).gameObject;
            textMeshPro = messageBox.transform.LudoFind("Text", includeInactive: true).GetComponent<TMP_Text>();
        }

        public void ShowMessage(string text)
        { 
            CloseMessage();
            showCoroutine = StartCoroutine(ShowMessageCoroutine(text));
        }

        public void CloseMessage()
        {
            if (showCoroutine != null)
            {
                StopCoroutine(showCoroutine);
                showCoroutine = null;
                messageBox.SetActive(false);
            }
        }

        IEnumerator ShowMessageCoroutine(string text)
        {
            textMeshPro.text = text;
            messageBox.SetActive(true);
            yield return new WaitForSeconds(2);
            messageBox.SetActive(false);
            showCoroutine = null;
        }

    }
}
