using Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools.Extensions;

namespace Interactions
{
    public class Custom : InteractionBase
    {
        //setting
        public CustomSubs.Scenario senario;
        public bool timeRestricted;
        public float time;
        public bool canManuallyLeave;
        //cached
        TMP_Text timeTMP;
        GameObject timeObject;
        GameObject exitObject;
        //runtime
        GameObject downHit;
        float timeLimitUpper;
        float timeLimitLower;
        float timeRemained;
        float timeRemainedToShow => Mathf.Clamp(timeRemained, 0f, timeLimitUpper-0.5f);
        Coroutine timeCoroutine;

        public void DataInit()
        {
            
        }

        public override void AStart()
        {
            timeTMP = transform.LudoFind("TimeLimit", includeInactive: true).GetComponent<TMP_Text>();
            timeObject = transform.LudoFind("TimeLimit", includeInactive: true).gameObject;
            exitObject = transform.LudoFind("Exit", includeInactive: true).gameObject;

            base.AStart();

            gameObject.SetActive(true);
            //module设置
            DialogueBox.singleton.Show(false);
            if (BackgroundManager.singleton.currentCoroutine != null)
            {
                StopCoroutine(BackgroundManager.singleton.currentCoroutine);
                if (BackgroundManager.singleton.currentFinishFunc != null) BackgroundManager.singleton.currentFinishFunc();
                BackgroundManager.singleton.currentCoroutine = null;
                BackgroundManager.singleton.currentFinishFunc = null;
            }
            if (BackgroundManager.singleton.currentBackground == senario.gameObject)
            {
                //什么都不做
            }
            else
            {
                //显示，会自动盖住原先的backGround。
                senario.gameObject.SetActive(true);
                BackgroundManager.singleton.currentBackground.SetActive(false);

            }
            
            Inventory.ScrollViewManager.singleton.transform.parent.parent.gameObject.SetActive(true);


            //根据不同的配置，设置东西
            {
                if (timeRestricted)
                {
                    timeObject.SetActive(true);

                    timeLimitUpper = time + 0.5f;
                    timeLimitLower = -0.5f;
                    timeRemained = timeLimitUpper;
                    timeCoroutine = StartCoroutine(TimeChange());
                }
                else
                {
                    timeObject.SetActive(false);
                }


                if (canManuallyLeave == true)
                {
                    exitObject.SetActive(true);
                }
                else
                {
                    exitObject.SetActive(false);
                }
            }
            
        }

        public override void AEnd()
        {
            base.AEnd();
            gameObject.SetActive(false);
            
            foreach (Transform t in InteractionData.singleton.transform)
            {
                CustomSubs.Scenario s = t.GetComponent<CustomSubs.Scenario>();
                if (s != null) s.gameObject.SetActive(false);

            }
            if (BackgroundManager.singleton.currentBackground == senario.gameObject)
            {
                senario.gameObject.SetActive(true);
            }
            else
            {
                BackgroundManager.singleton.currentBackground.SetActive(true);
            }
            Inventory.ScrollViewManager.singleton.transform.parent.parent.gameObject.SetActive(false);

            timeObject.SetActive(false);
            if (timeCoroutine != null)
            {
                StopCoroutine(timeCoroutine);
                timeCoroutine = null;
            }
            exitObject.SetActive(false);
        }

        public override void AInteract()
        {
            //Interact With UI In Scene
            
            GameObject currentHitUIInScene = null;
            {
                if (Input.GetMouseButtonDown(0))
                {
                    downHit = Tools.NoName.GetMouseHitOfUIInScene();
                    currentHitUIInScene = downHit;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    var upHit = Tools.NoName.GetMouseHitOfUIInScene();
                    currentHitUIInScene = upHit;
                    if (upHit != null && upHit == downHit)
                    {
                        UIInSceneInteract i = upHit.GetComponent<UIInSceneInteract>();
                        if (i != null) i.OnClick();
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    var currentHit = Tools.NoName.GetMouseHitOfUIInScene();
                    currentHitUIInScene = currentHit;
                    if (currentHit != null && currentHit == downHit)
                    {
                        UIInSceneInteract i = currentHit.GetComponent<UIInSceneInteract>();
                        if (i != null) i.OnHolding();
                    }
                }
            }
            if(currentHitUIInScene != null)
            {
                //print($"CurrentHitUIInScene: {currentHitUIInScene.name}");
                return;
            }
            

            //Interact In Scene
            {
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
                    var upHit = Tools.NoName.GetMouseHit();
                    if (upHit != null && upHit == downHit)
                    {
                        CustomSubs.Base b = upHit.GetComponent<CustomSubs.Base>();
                        b.OnClick();
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    var currentHit = Tools.NoName.GetMouseHit();
                    if (currentHit != null && currentHit == downHit)
                    {

                    }
                }
            }
            
        }

        //------------------------------------------------------------------//


        public void ManuallyExit()
        {
            AEnd();
        }

        private void Update()
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
                    timeCoroutine = null;
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
