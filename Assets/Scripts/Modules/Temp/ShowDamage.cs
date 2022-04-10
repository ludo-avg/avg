using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Tools.Extensions;

namespace Modules.Temp
{
    public class ShowDamage : MonoBehaviour
    {
        #region Static
        static public ShowDamage singleton = null;
        #endregion

        //Setting
        private Vector3 startPosition = new Vector3(0, -0.5f, 0);
        private Vector3 endPosition = new Vector3(0, 2.5f, 0);
        private float startScale = 1.5f;
        private float endScale = 0.1f;
        private float startAlpha = 1f;
        private float endAlpha = 0.2f;
        private float time = 2f;

        //Cached
        private GameObject parentNode;
        private SpriteRenderer heart;
        private TextMeshPro text;
        private Color startColor;
        private Color endColor;

        public void InitByModuleInit()
        {
            ShowDamage.singleton = this;
            gameObject.SetActive(false);



            {
                parentNode = this.gameObject;
                heart = parentNode.transform.LudoFind("Heart", includeInactive: true).GetComponent<SpriteRenderer>();
                text = parentNode.transform.LudoFind("Text", includeInactive: true).GetComponent<TextMeshPro>();

                if (heart.color != text.color)
                {
                    throw new System.Exception("In ShowDamage, heart.color should be the same as text.color.");
                }
                startColor = heart.color;
                endColor = heart.color;
                startColor.a = startAlpha;
                endColor.a = endAlpha;
            }
            
        }

        public void Show(int damage)
        {
            //移动
            //变小
            //透明

            text.text = "-" + damage.ToString();
            Tween positionTween = DOTween.To(
                () => startPosition,
                value => parentNode.transform.position = value,
                endPosition,
                time
                )
                .SetEase(Ease.InSine);
            Tween scaleTween = DOTween.To(
                () => startScale,
                value => parentNode.transform.localScale = new Vector3(value, value, 1),
                endScale,
                time
                )
                .SetEase(Ease.InCubic);
            Tween colorTweenHeart = DOTween.To(
                () => startColor,
                value => heart.color = value,
                endColor,
                time
                )
                .SetEase(Ease.InCubic);
            Tween colorTweenText = DOTween.To(
                () => startColor,
                value => text.color = value,
                endColor,
                time
                )
                .SetEase(Ease.InCubic);

            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(positionTween);
            mySequence.Join(scaleTween);
            mySequence.Join(colorTweenHeart);
            mySequence.Join(colorTweenText);
            mySequence.PrependCallback(() => gameObject.SetActive(true));
            mySequence.AppendCallback(() => gameObject.SetActive(false));
        }

    }
}
