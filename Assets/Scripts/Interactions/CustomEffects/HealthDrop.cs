using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Interactions.CustomEffects
{ 
    public class HealthDrop : Interactions.CustomEffect
    {
        #region static
        static private float firstPartTime = 0.3f;
        static private float time = 0.5f;

        static private float threeFlashPartTime = 0.15f;
        static float threeFlashTime = 0.5f;
        


        #endregion static
        public enum DamageType
        {
            Fixed,
            FromUserData
        }

        public enum FlashType
        {
            One,
            Three
        }

        public FlashType flashType = FlashType.One;
        public DamageType damageType;
        public int damage;
        public AudioClip clip = null;

        public void DataInit(DamageType damageType=DamageType.FromUserData, int damage = 1, AudioClip clip = null)
        {
            this.damageType = damageType;
            this.damage = damage;
            this.clip = clip;
        }

        public override void AStart()
        {
            base.AStart();
            gameObject.SetActive(true);
            StartCoroutine(Coroutine());
        }

        IEnumerator Coroutine()
        {
            if (flashType == FlashType.One)
            {
                //大概音效结束，就可以继续。不用管伤害数字。
                //可以定时间为0.5s。
                {
                    //白屏 0.1s
                    //音效 0.3s左右
                    //伤害数字 1s
                    Modules.Temp.ScreenFlash.singleton.Flash(new Color(1, 1, 1, 1), firstPartTime);
                    Modules.Sfx.singleton.PlayOneShot(clip);
                    ;
                    yield return new WaitForSeconds(firstPartTime);
                    int damageNum = damageType == DamageType.FromUserData ? UserData.singleton.lastHealthLoss : this.damage;
                    Modules.Temp.ShowDamage.singleton.Show(damageNum);
                }

                yield return new WaitForSeconds(time - firstPartTime);
                AEnd();
                gameObject.SetActive(false);
            }
            else if (flashType == FlashType.Three)
            {
                Modules.Temp.ScreenFlash.singleton.Flash(new Color(1, 0, 0, 1), threeFlashPartTime);
                Modules.Sfx.singleton.PlayOneShot(clip);
                yield return new WaitForSeconds(threeFlashPartTime);
                int damageNum = damageType == DamageType.FromUserData ? UserData.singleton.lastHealthLoss : this.damage;
                Modules.Temp.ShowDamage.singleton.Show(damageNum);

                yield return new WaitForSeconds(threeFlashTime - threeFlashPartTime);
                AEnd();
                Modules.Temp.ScreenFlash.singleton.Flash(new Color(1, 0, 0, 1), threeFlashPartTime);

                
                yield return new WaitForSeconds(threeFlashTime);
                Modules.Temp.ScreenFlash.singleton.Flash(new Color(1, 0, 0, 1), threeFlashPartTime);
                gameObject.SetActive(false);
            }
            
        }
    }
}
