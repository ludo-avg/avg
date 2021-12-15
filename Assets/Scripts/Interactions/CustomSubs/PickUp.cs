using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions.CustomSubs
{
    public class PickUp : Base
    {
        //setting
        public string showName = "";
        public UnityEvent callBack = null;
        public Inventory.Item item = null;
        //persist
        bool picked;


        public override void DataInit()
        {
            picked = false;
        }

        public override void EnterScenario()
        {
            if (influenceBy == null)
            {
                SetActiveByPicked();
            }
            else
            {
                bool trueOrFalse = influenceBy.GetComponent<IInfluence>().GetInfluence(this);

                if (trueOrFalse)
                {
                    SetActiveByPicked();
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public override void LeaveScenario()
        {

        }

        public override void OnClick()
        {
            picked = true;
            SetActiveByPicked();
            Modules.Message.singleton.ShowMessage("获得 " + showName);
            Inventory.Inventory.singleton.AddItem(item);
            Modules.Sfx.singleton.PlayPickUpSound();

            callBack.Invoke();
        }

        public override void InfluenceByIInfluence(bool trueOrFalse)
        {
            base.InfluenceByIInfluence(trueOrFalse);

            if (trueOrFalse)
            {
                SetActiveByPicked();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }


        //---------------------------------------------------------------
        private void SetActiveByPicked()
        {
            gameObject.SetActive(!picked);
        }

    }
}
