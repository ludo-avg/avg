using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Interactions.CustomEffects
{ 
    public class HealthDrop : Interactions.CustomEffect
    {
        #region static
        static private AudioClip clip = null; //Set By HealthDropSetting
        static private float time = 0.5f;

        static public void SetAudioClip(AudioClip clip)
        {
            HealthDrop.clip = clip;
        }
        #endregion static

        public int damage;
        
        public void DataInit(int damage = 1)
        {
            this.damage = damage;
        }

        public override void AStart()
        {
            base.AStart();
            gameObject.SetActive(true);
            StartCoroutine(Coroutine());
        }

        IEnumerator Coroutine()
        {
            //大概音效结束，就可以继续。不用管伤害数字。
            //可以定时间为0.5s。
            {
                //白屏 0.1s
                //音效 0.3s左右
                //伤害数字 1s
                Modules.Temp.ScreenFlash.singleton.Flash(new Color(1, 1, 1, 1), 0.3f);
                Modules.Sfx.singleton.PlayOneShot(clip);
                Modules.Temp.ShowDamage.singleton.Show(1);
            }

            yield return new WaitForSeconds(time);
            base.AEnd();
            gameObject.SetActive(false);
        }
    }
}
