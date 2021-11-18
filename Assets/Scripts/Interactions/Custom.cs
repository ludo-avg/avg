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

            DialogueBox.singleton.Show(false);
            gameObject.SetActive(true);
            senario.gameObject.SetActive(true);
            Inventory.ScrollViewManager.singleton.transform.parent.parent.gameObject.SetActive(true);

            if (timeRestricted)
            {
                timeObject.SetActive(true);

                timeLimitUpper = time + 0.5f;
                timeLimitLower = -0.5f;
                timeRemained = timeLimitUpper;
                timeCoroutine = StartCoroutine(TimeChange());
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

        public override void AEnd()
        {
            base.AEnd();
            gameObject.SetActive(false);
            foreach(Transform t in InteractionData.singleton.transform)
            {
                CustomSubs.Scenario s = t.GetComponent<CustomSubs.Scenario>();
                if (s != null) s.gameObject.SetActive(false);

            }
            Inventory.ScrollViewManager.singleton.transform.parent.parent.gameObject.SetActive(fals);

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
