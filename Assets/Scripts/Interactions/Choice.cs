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
    public class Choice : InteractionBase
    {
        #region Type
        protected enum LR
        {
            Left,
            Right
        }

        protected class Button
        {
            public GameObject button;
            public GameObject ImageUp;
            public GameObject ImageDown;
        }

        protected enum EndState
        {
            Left,
            Right,
            OutOfTime
        }
        #endregion

        //setting
        public string text;
        public AudioClip audioClip;
        public UnityEvent initCall = null;
        public UnityEvent leftCall = null;
        public UnityEvent rightCall = null;

        //cached
        protected Dictionary<LR, Button> buttons;
        //runtime
        GameObject downHit;

        public virtual void DataInit(string text, UnityEvent leftCall = null, UnityEvent rightCall = null, AudioClip audioClip = null)
        {
            this.text = text;
            this.leftCall = leftCall;
            this.rightCall = rightCall;
            this.audioClip = audioClip;
        }

        public override void AStart()
        {
            base.AStart();

            //1
            buttons = new Dictionary<LR, Button>();
            Button left = new Button();
            left.button = transform.Find("Left").gameObject;
            left.ImageDown = left.button.transform.Find("ImageDown").gameObject;
            left.ImageUp = left.button.transform.Find("ImageUp").gameObject;
            Button right = new Button();
            right.button = transform.Find("Right").gameObject;
            right.ImageDown = right.button.transform.Find("ImageDown").gameObject;
            right.ImageUp = right.button.transform.Find("ImageUp").gameObject;
            ;
            buttons.Add(LR.Left, left);
            buttons.Add(LR.Right, right);

            //2
            downHit = null;

            //3
            gameObject.SetActive(true);

            //4
            DialogueBox.singleton.Show(true);
            DialogueTypeWriter.singleton.OutputText(text);
            Voice voice = Voice.singleton;
            voice.StopVoice();
            if (audioClip != null)
            {
                voice.PlayVoice(audioClip);
            }

            //5
            if (initCall != null)
            {
                initCall.Invoke();
            }
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

            base.AEnd();
            gameObject.SetActive(false);
            Voice voice = Voice.singleton;
            voice.StopVoice();

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
        }

        public override void AInteract()
        {
            /*  ◑ 音效
                ✓ down up，各种处理
                ✓ 需要响应普通点击
                ✓ 进入时，可能鼠标已经在down状态
            */


            #region SubFunction
            LR? MouseHitToLR(GameObject hit)
            {
                if (hit.name == "Left")
                    return LR.Left;
                if (hit.name == "Right")
                    return LR.Right;
                return null;
            }

            void SetButtonRepresentOneButtonDown(GameObject hit)
            {
                LR? hitLR = MouseHitToLR(hit);
                if (hitLR.HasValue)
                {
                    LR lr = hitLR.Value;
                    buttons[lr].ImageDown.SetActive(true);
                    buttons[lr].ImageUp.SetActive(false);

                    LR theOther = (lr == LR.Left) ? LR.Right : LR.Left;
                    buttons[theOther].ImageDown.SetActive(false);
                    buttons[theOther].ImageUp.SetActive(true);
                }
            }

            void SetButtonRepresentAllButtonUp()
            {
                buttons[LR.Left].ImageDown.SetActive(false);
                buttons[LR.Left].ImageUp.SetActive(true);
                buttons[LR.Right].ImageDown.SetActive(false);
                buttons[LR.Right].ImageUp.SetActive(true);
            }
            #endregion

            //1. 每一帧都执行
            {
                GameObject currentHit = null;
                if (Input.GetMouseButton(0))
                {
                    currentHit = Tools.NoName.GetMouseHit();
                }

                if (currentHit != null && currentHit == downHit)
                {
                    SetButtonRepresentOneButtonDown(currentHit);
                }
                else SetButtonRepresentAllButtonUp();
            }
            

            //2. 有Down、Up事件时的相应
            if (Input.GetMouseButtonDown(0))
            {
                downHit = Tools.NoName.GetMouseHit();

                if (DialogueTypeWriter.singleton.state == DialogueTypeWriter.TypewriterState.Interrupted
                    && downHit == null)
                {
                    DialogueTypeWriter.singleton.CompleteOutput();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                GameObject upHit = Tools.NoName.GetMouseHit();
                if (upHit != null && upHit == downHit)
                {
                    SetButtonRepresentOneButtonDown(upHit);
                }
                else SetButtonRepresentAllButtonUp();

                if (upHit != null && upHit == downHit) AEnd();
            }
        }
    }
}
