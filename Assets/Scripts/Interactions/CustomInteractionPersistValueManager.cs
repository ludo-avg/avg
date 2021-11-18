using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class CustomInteractionPersistValueManager : MonoBehaviour
    {
        public CustomSubs.Scenario[] senarios = null;
        
        void Start()
        {
            foreach (var senario in senarios)
            {
                Transform transform = senario.transform;
                foreach (Transform t in transform)
                {
                    var customSub = t.GetComponent<CustomSubs.Base>();
                    if (customSub != null)
                    {
                        customSub.DataInit();
                    }
                }
            }
        }
    }
}