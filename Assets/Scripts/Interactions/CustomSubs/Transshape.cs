using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Extensions;

namespace Interactions.CustomSubs
{
    public class Transshape : Base, IInfluence
    {
        #region TypeDefine
        public enum Shape0Or1
        {
            Shape0,
            Shape1
        }

        [Serializable]
        public class InfluenceVisible
        {
            public Shape0Or1 shape0or1;
            public Base customSub;
        }

        #endregion
        //Setting
        public InfluenceVisible[] influenceVisibles;

        //persist
        GameObject shape0;
        GameObject shape1;
        Shape0Or1 shapeState;
        BoxCollider boxCollider;
        BoxCollider boxColliderShape0;
        BoxCollider boxColliderShape1;

        public override void DataInit()
        {
            shape0 = transform.LudoFind("Shape0", includeInactive: true).gameObject;
            shape1 = transform.LudoFind("Shape1", includeInactive: true).gameObject;
            shapeState = Shape0Or1.Shape0;
            boxCollider = GetComponent<BoxCollider>();
            boxColliderShape0 = shape0.GetComponent<BoxCollider>();
            boxColliderShape1 = shape1.GetComponent<BoxCollider>();
        }

        public override void EnterScenario()
        {
            if (influenceBy == null)
            {
                gameObject.SetActive(true);
                SetVisibilityAndColliderByShape();
            }
            else
            {
                bool trueOrFalse = influenceBy.GetComponent<IInfluence>().GetInfluence(this);

                if (trueOrFalse)
                {
                    gameObject.SetActive(true);
                    SetVisibilityAndColliderByShape();
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public override void LeaveScenario()
        {
            gameObject.SetActive(false);
        }

        public override void OnClick()
        {
            if (shapeState == Shape0Or1.Shape0)
            {
                shapeState = Shape0Or1.Shape1;
            }
            else
            {
                shapeState = Shape0Or1.Shape0;
            }
            SetVisibilityAndColliderByShape();
        }

        //--------------------------------------------------------------------------------------
        private void Update()
        {
            foreach (var pair in influenceVisibles)
            {
                pair.customSub.InfluenceByIInfluence(pair.shape0or1 == shapeState);
            }
        }

        private void SetVisibilityAndColliderByShape()
        {
            if (shapeState == Shape0Or1.Shape0)
            {
                shape0.SetActive(true);
                shape1.SetActive(false);
                boxCollider.center = boxColliderShape0.center;
                boxCollider.size = boxColliderShape0.size;
            }
            else
            {
                shape0.SetActive(false);
                shape1.SetActive(true);

                boxCollider.center = boxColliderShape1.center;
                boxCollider.size = boxColliderShape1.size;
            }
            
        }

        bool IInfluence.GetInfluence(Base customSub)
        {
            foreach (var pair in influenceVisibles)
            {
                if (pair.customSub == customSub)
                {
                    return pair.shape0or1 == shapeState;
                }
            }
            return false;
        }

    }
}