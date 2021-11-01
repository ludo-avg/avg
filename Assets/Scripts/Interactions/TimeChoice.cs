using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

namespace Interactions
{
    public class TimeChoice : MonoBehaviour
    {
        #region Type
        class Button //TimeChoiceButton
        {
            public GameObject button;
            public GameObject ImageUp;
            public GameObject ImageDown;
        }

        enum EndState
        {
            Left,
            Right,
            OutOfTime
        }
        #endregion

        //setting
        string text = "阿雯反应极快，瞬间就做出了判断：";
        string textChoiceSelected1 = "只见老师使出了一招左右幻影投射技巧，粉笔头正中阿雯眉心。";
        string textChoiceSelected2 = "老师得意的吹了手上的粉笔灰。";
        string textOutOfRange1 = "只见老师使出了一招左右幻影投射技巧，粉笔头同时在左右出现了一道幻影，阿雯丝毫来不及反应，但是粉笔头奇异的从阿雯的耳边飞过。";
        string textOutOfRange2 = "老师生气的瞪了阿雯一眼。";
        /*
            时间限制为5s，
            但真实的时间限制是6s。
        
            一开始，送玩家0.5s，从5.5开始。时间到了后再多走0.5s才结束。
        */
        float timeLimitUpper = 5.5f;
        float timeLimitLower = -0.5f;

        //cached
        TMP_Text timeTmpro;
        Dictionary<string, Button> buttons;
        //runtime
        //
        [NonSerialized] public bool ended = false;
        //
        [ShowNonSerializedField] float timeRemained;
        float timeRemainedToShow => Mathf.Clamp(timeRemained, 0f, 5f);
        //
        string downHit;
        

        //=================================================================================================
        public void InteractionStart()
        {
            //1
            timeTmpro = transform.Find("TimeLimit").GetComponent<TMP_Text>();

            buttons = new Dictionary<string, Button>();
            Button left = new Button();
            left.button = transform.Find("Left").gameObject;
            left.ImageDown = left.button.transform.Find("ImageDown").gameObject;
            left.ImageUp = left.button.transform.Find("ImageUp").gameObject;
            Button right = new Button();
            right.button = transform.Find("Right").gameObject;
            right.ImageDown = right.button.transform.Find("ImageDown").gameObject;
            right.ImageUp = right.button.transform.Find("ImageUp").gameObject;

            buttons.Add("Left", left);
            buttons.Add("Right", right);

            //2
            ended = false;
            timeRemained = timeLimitUpper;
            downHit = null;

            //3
            gameObject.SetActive(true);

            //4
            DialogueTypeWriter.singleton.OutputText(text);
            StartCoroutine(TimeChange());
        }

        void InteractionEnd()
        {
            gameObject.SetActive(false);
            ended = true;

            //------------------

            /*
                ✓ 根据down，up。得到玩家选项。
                ✓ 根据玩家选项，插入不同的dialogue。
            */
            EndState endState = EndState.OutOfTime;
            bool leftDown = GetLeftDown();
            bool rightDown = GetRightDown();
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

            if (endState == EndState.Right || endState == EndState.Left)
            {
                InteractionList.singleton.Insert(InteractionManager.singleton.currentNum + 1, new Dialogue(textChoiceSelected1));
                InteractionList.singleton.Insert(InteractionManager.singleton.currentNum + 2, new Dialogue(textChoiceSelected2));
            }
            else
            {
                InteractionList.singleton.Insert(InteractionManager.singleton.currentNum + 1, new Dialogue(textOutOfRange1));
                InteractionList.singleton.Insert(InteractionManager.singleton.currentNum + 2, new Dialogue(textOutOfRange2));
            }
        }

        public void InteractionInteract()
        {
            /*  音效
                ✓ down up，各种处理
                ✓ 需要响应普通点击
                ✓ 进入时，可能鼠标已经在down状态
            */
            if (Input.GetMouseButtonDown(0))
            {
                downHit = GetMouseHit();

                if (DialogueTypeWriter.singleton.state == DialogueTypeWriter.TypewriterState.Interrupted 
                    && downHit == null)
                {
                    DialogueTypeWriter.singleton.CompleteOutput();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                string upHit = GetMouseHit();
                if (upHit != null && upHit == downHit)
                {
                    OneButtonDown(upHit);
                    InteractionEnd();
                }
                else AllButtonUp();
            }

            if (Input.GetMouseButton(0))
            {
                string currentHit = GetMouseHit();
                if (currentHit != null && currentHit == downHit)
                {
                    OneButtonDown(currentHit);
                }
                else AllButtonUp();
            }
            else AllButtonUp();
        }

        private string GetMouseHit()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform != null)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    return hitObject.name;
                }
                else
                    return null;
            }
            return null;
        }

        private void OneButtonDown(string hit)
        {
            if (hit == "Left" || hit == "Right")
            {
                buttons[hit].ImageDown.SetActive(true);
                buttons[hit].ImageUp.SetActive(false);

                string theOther = (hit == "Left") ? "Right" : "Left";
                buttons[theOther].ImageDown.SetActive(false);
                buttons[theOther].ImageUp.SetActive(true);
            }
        }

        private void AllButtonUp()
        {
            buttons["Left"].ImageDown.SetActive(false);
            buttons["Left"].ImageUp.SetActive(true);
            buttons["Right"].ImageDown.SetActive(false);
            buttons["Right"].ImageUp.SetActive(true);
        }

        private bool GetLeftDown()
        {
            return buttons["Left"].ImageDown.activeSelf;
        }

        private bool GetRightDown()
        {
            return buttons["Right"].ImageDown.activeSelf;
        }

        //===================================================================================

        private void Update()
        {
            SetupTimeString();

            if (timeRemained < timeLimitLower)
            {
                InteractionEnd();
            }
        }

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

        void SetupTimeString()
        {
            string timeString = "限时" + timeRemainedToShow.ToString("N1") + "秒";
            timeTmpro.text = timeString;
        }
    }
}
