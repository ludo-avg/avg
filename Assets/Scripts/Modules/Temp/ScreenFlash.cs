using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Modules.Temp
{
    public class ScreenFlash : MonoBehaviour
    {
        #region Static
        static public ScreenFlash singleton = null;
        #endregion

        SpriteRenderer sr;
        public void InitByModuleInit()
        {
            ScreenFlash.singleton = this;
            gameObject.SetActive(false);

            sr = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// default value for color is Color.white
        /// </summary>
        /// <param name="color"></param>
        /// <param name="time"></param>
        public void Flash(Color? color = null, float time = 0.1f)
        {
            color = color ?? Color.white;
            Color colorV = color.Value;

            Tween tween1 = DOTween.To(
                () => new Color(colorV.r, colorV.g, colorV.b, 0),
                value => sr.color = value,
                colorV,
                time / 2
                );
            Tween tween2 = DOTween.To(
                () => colorV,
                value => sr.color = value,
                new Color(colorV.r, colorV.g, colorV.b, 0),
                time / 2
                );

            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(tween1);
            mySequence.Append(tween2);
            mySequence.PrependCallback(() => gameObject.SetActive(true));
            mySequence.AppendCallback(() => gameObject.SetActive(false));
        }

    }
}
