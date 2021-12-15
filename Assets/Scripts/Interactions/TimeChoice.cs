using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using NaughtyAttributes;
using Modules;

namespace Interactions
{
    public class TimeChoice : Choice
    {
        //setting
        [SerializeField] float time;
            /*
                假如，时间限制为5s，
                真实的时间限制是6s。

                一开始，送玩家0.5s，从5.5开始。时间到了后再多走0.5s才结束。
            */
        public UnityEvent overTimeCall = null;
        //cached
        TMP_Text timeTMP;
        //runtime
        float timeLimitUpper;
        float timeLimitLower;
        float timeRemained;
        float timeRemainedToShow => Mathf.Clamp(timeRemained, 0f, timeLimitUpper - 0.5f);

        public void DataInit(string text, float time, UnityEvent leftCall = null, UnityEvent rightCall = null, AudioClip audioClip = null, UnityEvent overTimeCall = null)
        {
            base.DataInit(text, leftCall, rightCall, audioClip);
            this.time = time;
            this.overTimeCall = overTimeCall;
        }

        public override void DataInit(string text, UnityEvent leftCall = null, UnityEvent rightCall = null, AudioClip audioClip = null)
        {
            DataInit(text, 5f);
        }

        public override void AStart()
        {
            base.AStart();

            timeLimitUpper = time + 0.5f;
            timeLimitLower = -0.5f;
            timeTMP = transform.Find("TimeLimit").GetComponent<TMP_Text>();
            timeRemained = timeLimitUpper;

            StartCoroutine(TimeChange());
        }

        public override void AEnd()
        {
            #region SubFunction
            bool IsLeftDown()
            {
                return buttons[LR.Left].ImageDown.activeSelf;
            }

            bool IsRightDown()
            {
                return buttons[LR.Right].ImageDown.activeSelf;
            }
            #endregion


            //这里需要Call InteractionBase的AEnd，但是call不了。以后想办法。
            ended = true;
            //

            gameObject.SetActive(false);

            EndState endState = EndState.OutOfTime;
            {
                bool leftDown = IsLeftDown();
                bool rightDown = IsRightDown();
                if (leftDown && rightDown)
                {
                    Debug.LogError("left and right both down!");
                }
                if (leftDown && !rightDown)
                {
                    endState = EndState.Left;
                }
                else if (!leftDown && rightDown)
                {
                    endState = EndState.Right;
                }
            }

            if (endState == EndState.Left)
            {
                if (leftCall != null) leftCall.Invoke();
            }
            else if (endState == EndState.Right)
            {
                if (rightCall != null) rightCall.Invoke();
            }
            else if (endState == EndState.OutOfTime)
            {
                if (overTimeCall != null) overTimeCall.Invoke();
            }
        }

        public override void AInteract()
        {
            base.AInteract();
        }

        public override void AUpdate()
        {
            #region SubFunc
            void SetupTimeString()
            {
                string timeString = "限时" + timeRemainedToShow.ToString("N1") + "秒";
                timeTMP.text = timeString;
            }
            #endregion

            SetupTimeString();
            if (timeRemained < timeLimitLower)
            {
                AEnd();
            }
        }


        /*---------------------------------------------------*/
        /*
                          Level2 Functions
        */
        /*---------------------------------------------------*/

        IEnumerator TimeChange()
        {
            float startTime = Time.time;
            while (true)
            {
                float currentTime = Time.time;
                float timePassed = (currentTime - startTime);
                timeRemained = timeLimitUpper - timePassed;

                if (timeRemained < timeLimitLower)
                {
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
