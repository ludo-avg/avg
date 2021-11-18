using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.CustomSubs
{
    public class Scenario : MonoBehaviour
    {

        [NonSerialized] public List<Base> subs;
        private void OnEnable()
        {
            OnEnterSenario();
        }

        private void OnDisable()
        {
            OnLeaveSenario();
        }

        void OnEnterSenario()
        {
            subs = new List<Base>();
            foreach (Transform t in transform)
            {
                if(t.GetComponent<Base>() != null)
                {
                    subs.Add(t.GetComponent<Base>());
                }
            }

            foreach(var sub in subs)
            {
                sub.EnterScenario();
            }
        }

        void OnLeaveSenario()
        {
            foreach (var sub in subs)
            {
                sub.LeaveScenario();
            }
            Modules.Message.singleton.CloseMessage();
        }
    }
}