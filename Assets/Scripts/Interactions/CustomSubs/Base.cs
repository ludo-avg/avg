using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.CustomSubs
{
    public class Base : MonoBehaviour
    {
        //setting
        public GameObject influenceBy = null;

        //持久化数据的数值化
        public virtual void DataInit()
        {

        }

        //进入场景时，执行的操作
        public virtual void EnterScenario()
        {

        }

        //离开场景时，执行的操作
        public virtual void LeaveScenario()
        {

        }

        //Click
        public virtual void OnClick()
        {

        }

        public virtual void InfluenceByIInfluence(bool trueOrFalse)
        {
            IInfluence temp = influenceBy.GetComponent<IInfluence>();
        }
    }
}

