using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Modules;

namespace Interactions
{
    public class ChangeBackground : InteractionBase
    {
        //setting
        [SerializeField] private GameObject newBackgroundObj;
        //cached
        public Sprite newBackground { private set; get; }
        //runtime
        bool backgroundFinish;
        bool toChangeFinish;

        public void DataInit(GameObject backGround)
        {
            newBackgroundObj = backGround;
        }

        public override void AStart()
        {
            base.AStart();
            gameObject.SetActive(true);

            newBackground = newBackgroundObj.GetComponent<SpriteRenderer>().sprite;
            var backGroundSR = Background.singleton.sr;
            var toChangeSR = BackgroundToChange.singleton.sr;
            var toChangeObj = BackgroundToChange.singleton.gameObject;
            
            toChangeSR.sprite = newBackground;
            DialogueBox.singleton.Show(false);
            ;
            toChangeObj.SetActive(true);
            backgroundFinish = false;
            toChangeFinish = false;
            var color0 = new Color(1, 1, 1, 0);
            var color1 = new Color(1, 1, 1, 1);
            backGroundSR.DOColor(color0, 0.8f).OnKill(() => backgroundFinish = true);
            toChangeSR.color = color1; //toChangeSR.color = color0;
            toChangeSR.DOColor(color1, 0.8f).OnKill(() => toChangeFinish = true);
            StartCoroutine(ChangeBackgroundFinish());

            IEnumerator ChangeBackgroundFinish()
            {
                while (!backgroundFinish || !toChangeFinish)
                {
                    yield return null;
                }
                backGroundSR.sprite = newBackground;
                backGroundSR.color = new Color(1, 1, 1, 1);
                toChangeObj.SetActive(false);

                gameObject.SetActive(false);
                AEnd();
            }
            
        }

    }
}

