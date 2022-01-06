using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class CustomInteractionPersistValueManager : MonoBehaviour
    {
        public PointAndClickSubs.Scenario[] senarios = null;
        
        void Start()
        {
            foreach (var senario in senarios)
            {
                Transform transform = senario.transform;
                foreach (Transform t in transform)
                {
                    var customSub = t.GetComponent<PointAndClickSubs.Base>();
                    if (customSub != null)
                    {
                        customSub.DataInit();
                    }
                }
            }
        }
    }
}