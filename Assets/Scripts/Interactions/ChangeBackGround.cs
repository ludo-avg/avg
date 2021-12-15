using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Modules;

namespace Interactions
{
    public class ChangeBackground : InteractionBase
    {
        /*
            调整 AEnd(); 的执行位置。可以让 ChangeBackground 执行时，后面的交互等待或不等待。
            如果在AStart() 里执行 AEnd()，后面的交互，就不用等待 ChangeBackground 完成。
            如果在Finish() 里执行 AEnd()，就需要等待。
            -- 当前代码是在 Finish() 里执行 AEnd()。

            gameObject.SetActive(false); 目前 必须放在 Finish()，不能放在AStart()。
            因为，不active之后，coroutine也同时停了。
            应该考虑把Coroutine放在BackgroundManager里执行。就可以解决这个问题了。
            ；
            一个附加问题是。gameObject的avtive与否，究竟表达着什么？
            到了现在这个程度，其实有点不清楚了。

        */
        #region TypeDefine
        public enum Type
        {
            Simple = 0,
            Complex = 1
        }
        public enum InType
        {
            Simple = 0,
            Complex = 1,
            AutoDecide
        }
        #endregion
        //setting
        [NonSerialized] float changeTime = 0.8f;
        [SerializeField] private GameObject background;
        [SerializeField] private bool instantChange;
        //cached
        [NonSerialized] public Type type;
        //runtime
        public bool finish { private set; get; }

        public void DataInit(GameObject background, InType inType = InType.AutoDecide, bool instantChange = false)
        {
            this.background = background;

            if (inType != InType.AutoDecide)
            {
                type = (Type)inType;
            }
            else
            {
                SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    type = Type.Simple;
                }
                else
                {
                    type = Type.Complex;
                }
            }

            this.instantChange = instantChange;
        }

        public override void AStart()
        {
            base.AStart();
            gameObject.SetActive(true);
            DialogueBox.singleton.Show(false);

            if (BackgroundManager.singleton.currentCoroutine != null)
            {
                StopCoroutine(BackgroundManager.singleton.currentCoroutine);
                if (BackgroundManager.singleton.currentFinishFunc != null) BackgroundManager.singleton.currentFinishFunc();

                BackgroundManager.singleton.currentCoroutine = null;
                BackgroundManager.singleton.currentFinishFunc = null;
            }

            /*
                有2种替换方式
                1. 瞬间替换
                2. 渐变
                如果 
                    (instantChange为true) 
                    或 
                    (BackgroundChanger.singleton.currentType是Complex）
                        Complex的current，没法做渐变消失
                就瞬间替换。
                其他情况，渐变。
            */
            if (instantChange
                || 
                (BackgroundManager.singleton.currentType == BackgroundManager.Type.Complex) )
            {
                BackgroundManager.singleton.currentBackground.SetActive(false);
                background.SetActive(true);
                Finish();

                BackgroundManager.singleton.currentBackground = background;
                BackgroundManager.singleton.currentType = (BackgroundManager.Type)type;
                BackgroundManager.singleton.currentCoroutine = null;
                BackgroundManager.singleton.currentFinishFunc = null;
            }
            else
            {
                BackgroundManager.singleton.currentBackground.SetActive(false);
                background.SetActive(true);
                BackgroundManager.singleton.foregroundSR.gameObject.SetActive(true);
                BackgroundManager.singleton.foregroundSR.sprite = BackgroundManager.singleton.currentBackground.GetComponent<SpriteRenderer>().sprite;
                BackgroundManager.singleton.foregroundSR.color = new Color(1, 1, 1, 1);
                

                BackgroundManager.singleton.currentBackground = background;
                BackgroundManager.singleton.currentType = (BackgroundManager.Type)type;
                BackgroundManager.singleton.currentCoroutine = StartCoroutine(ChangeBackgroundCoroutine());
                BackgroundManager.singleton.currentFinishFunc = Finish;
            }
        }

        IEnumerator ChangeBackgroundCoroutine()
        {
            float timeStart = Time.time;
            while (Time.time - timeStart <= changeTime)
            {
                float timePassed = Time.time - timeStart;
                BackgroundManager.singleton.foregroundSR.color = new Color(1, 1, 1, 1 - timePassed/changeTime);
                yield return null;
            }
            Finish();
        }

        public void Finish()
        {
            BackgroundManager.singleton.foregroundSR.gameObject.SetActive(false);

            BackgroundManager.singleton.currentCoroutine = null;
            BackgroundManager.singleton.currentFinishFunc = null;
            
            gameObject.SetActive(false);
            AEnd();
        }

    }
}

