using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.PointAndClickSubs
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
        public AudioClip noItemAudio;
        public AudioClip useItemAudio;
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
            bool hasItem = Inventory.Inventory.singleton.HasItem(item);
            if (hasItem)
            {
                Modules.ConfirmBox.singleton.Show("是否使用 " + item.name + "？", item, this);
            }
            else
            {
                Modules.Message.singleton.ShowMessage(prompt);
                if (noItemAudio != null) Modules.Sfx.singleton.PlayOneShot(noItemAudio);
            }
        }

        public void AfterInteract()
        {
            state = PreOrAfter.After;
            gameObject.SetActive(false);

            Modules.Message.singleton.ShowMessage(right);
            Inventory.Inventory.singleton.Use(item);
            if (useItemAudio != null) Modules.Sfx.singleton.PlayOneShot(useItemAudio);

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