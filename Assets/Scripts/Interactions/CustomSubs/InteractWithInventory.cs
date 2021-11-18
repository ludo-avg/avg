using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.CustomSubs
{
    public class InteractWithInventory : Base, IInfluence
    {
        #region Type
        public enum PreOrAfter
        {
            Pre,
            After
        }

        #endregion
        //Setting
        public string prompt;
        public string right;
        public Inventory.Item item;
        public Base[] influenceSubs;
        //persist
        [NonSerialized] public PreOrAfter state;


        public override void DataInit()
        {
            state = PreOrAfter.Pre;
        }

        public override void EnterScenario()
        {
            if (state == PreOrAfter.Pre)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }

        }

        public override void LeaveScenario()
        {
            gameObject.SetActive(false);
        }

        public override void OnClick()
        {
            var selectedItem = Inventory.Inventory.singleton.GetSelectedItem();
            if (selectedItem == item)
            {
                AfterInteract();
            }
            else
            {
                Modules.Message.singleton.ShowMessage(prompt);
            }
        }

        private void AfterInteract()
        {
            state = PreOrAfter.After;
            gameObject.SetActive(false);

            Modules.Message.singleton.ShowMessage(right);
            Inventory.Inventory.singleton.Use();

            foreach (var influenced in influenceSubs)
            {
                influenced.gameObject.GetComponent<Base>().EnterScenario();
            }
        }

        bool IInfluence.GetInfluence(Base customSub)
        {
            foreach(var influenced in influenceSubs)
            {
                if (influenced == customSub)
                {
                    return (state == PreOrAfter.After);
                }
            }
            return false;
        }
    }
}