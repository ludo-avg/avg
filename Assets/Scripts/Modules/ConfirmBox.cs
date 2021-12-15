using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools.Extensions;

namespace Modules
{
    public class ConfirmBox : MonoBehaviour
    {
        #region
        public static ConfirmBox singleton = null;
        private void Awake()
        {
            singleton = this;
        }
        #endregion


        GameObject box;
        TMP_Text textTMP;
        Inventory.Item item;
        Interactions.CustomSubs.InteractWithInventory interact;

        void Start()
        {
            box = transform.LudoFind("Box", includeInactive: true).gameObject;
            textTMP = box.transform.LudoFind("Text", includeInactive: true).GetComponent<TMP_Text>();
        }

        void Update()
        {

        }

        public void Show(string text, Inventory.Item item, Interactions.CustomSubs.InteractWithInventory interact)
        {
            textTMP.text = text;
            this.item = item;
            this.interact = interact;
            box.SetActive(true);
        }

        public void Close()
        {
            box.SetActive(false);
        }

        public void Yes()
        {
            Inventory.Inventory.singleton.Use(item);
            interact.AfterInteract();
            Close();
        }

        public void No()
        {
            Close();
        }
    }
}