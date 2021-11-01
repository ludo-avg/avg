using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

namespace Interactions
{
    public class Choice : MonoBehaviour
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
        string text = "阿雯选择了：";
        /*
        string textChoiceLeft1 = "我正好有10块钱，就选这个吧。";
        string textChoiceLeft2 = "此时旁边跳出来一个身影，正是阿雯学校的教导主任。";
        string textChoiceLeft3 = "我就知道开家长会一定会有人来租爹，给我压回学校。";
        string textChoiceLeft4 = "阿雯就这样结束了愉快的暑假。";
        string textRight1 = "阿雯看到500的牌子脑袋有点转不过弯，没想明白为啥要选500的，说话都开始磕巴了。";
        string textRight2 = "5。。。5。。。";
        string textRight3 = "5块钱成交！";
        */

        //cached
        Dictionary<string, Button> buttons;
        //runtime
        //
        [NonSerialized] public bool ended = false;
        //
        string downHit;


        //=================================================================================================
        public void InteractionStart()
        {
            //1
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
            downHit = null;

            //3
            gameObject.SetActive(true);

            //4
            DialogueTypeWriter.singleton.OutputText(text);
        }

        void InteractionEnd()
        {
            gameObject.SetActive(false);
            ended = true;

            //------------------

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

            if (endState == EndState.Left)
            {
                //
            }
            else if (endState == EndState.Right)
            {
                //
            }
        }

        public void InteractionInteract()
        {
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
    }
}
