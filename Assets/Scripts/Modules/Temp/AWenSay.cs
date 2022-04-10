using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Tools.Extensions;

namespace Modules.Temp
{
    public class AWenSay : MonoBehaviour
    {
        #region Singleton
        static public AWenSay singleton;

        private void Awake()
        {
            singleton = this;
        }


        #endregion

        //Setting
        Vector3 aWenSayPositon = new Vector3(-7.67f, -4.44f, 0f);
        Vector3 aWenHidePosition = new Vector3(-8.7f, -7.44f, 0f);
        float aWenMoveTime = 1f;
        float aWenHideDelay = 3f;
        //
        Vector3 textStartPosition = new Vector3(-5.78f, -2.53f, 0f);
        float textXIncrement = 0.4f;
        float textYIncrement = -0.6f;
        float textSpawnInterval = 0.1f;
        int textSpawnLineCharacterCount = 11;
        float textFactoryDealy = 0.01f;

        public GameObject characterPrefab;

        //Cached
        GameObject aWen;
        GameObject textFactory;
        Tween aWenMoveTween;
        Coroutine aWenHideCoroutine;
        Coroutine textFactoryCoroutine;

        void Start()
        {
            aWen = transform.LudoFind("AWen", includeInactive: true).gameObject;
            textFactory = transform.LudoFind("TextFactory", includeInactive: true).gameObject;
            aWen.transform.position = aWenHidePosition;
        }

        public void Say(string text, AudioClip audio)
        {
            aWen.transform.position = aWenHidePosition;
            if (aWenMoveTween != null)
            {
                aWenMoveTween.Kill();
                aWenMoveTween = null;
            }

            aWenMoveTween =
                DOTween.To(
                        () => aWenHidePosition,
                        value => aWen.transform.position = value,
                        aWenSayPositon,
                        aWenMoveTime
                    )
                    .OnComplete(() => aWenMoveTween = null);
            ;

            
            if (Modules.Voice.singleton.GetState() != Modules.Voice.State.Stopped)
            {
                Modules.Voice.singleton.StopVoice();
            }
            if (aWenHideCoroutine != null)
            {
                StopCoroutine(aWenHideCoroutine);
                aWenHideCoroutine = null;
            }
            if (textFactoryCoroutine != null)
            {
                StopCoroutine(textFactoryCoroutine);
                textFactoryCoroutine = null;
            }
            aWenHideCoroutine = StartCoroutine(AWenHide());
            textFactoryCoroutine = StartCoroutine(TextFactory(text, audio));
        }

        public void StopSaying()
        {
            aWen.transform.position = aWenHidePosition;
            if (aWenMoveTween != null)
            {
                aWenMoveTween.Kill();
                aWenMoveTween = null;
            }
            if (aWenHideCoroutine != null)
            {
                StopCoroutine(aWenHideCoroutine);
                aWenHideCoroutine = null;
            }
            if (textFactoryCoroutine != null)
            {
                StopCoroutine(textFactoryCoroutine);
                textFactoryCoroutine = null;
            }
            if (Modules.Voice.singleton.GetState() != Modules.Voice.State.Stopped)
            {
                Modules.Voice.singleton.StopVoice();
            }
        }

        IEnumerator AWenHide()
        {
            yield return new WaitForSeconds(aWenHideDelay);

            aWen.transform.position = aWenSayPositon;
            if (aWenMoveTween != null)
            {
                aWenMoveTween.Kill();
                aWenMoveTween = null;
            }

            aWenMoveTween =
                DOTween.To(
                        () => aWenSayPositon,
                        value => aWen.transform.position = value,
                        aWenHidePosition,
                        aWenMoveTime
                    )
                    .OnComplete(() => aWenMoveTween = null);
            aWenHideCoroutine = null;
        }

        IEnumerator TextFactory(string text, AudioClip audio)
        {
            yield return new WaitForSeconds(textFactoryDealy);

            Modules.Voice.singleton.PlayVoice(audio);

            for (int i = 0; i < text.Length; i++)
            {
                yield return new WaitForSeconds(textSpawnInterval);

                char c = text[i];
                int x = i % textSpawnLineCharacterCount;
                int y = i / textSpawnLineCharacterCount;
                float xPos = textStartPosition.x + x * textXIncrement;
                float yPos = textStartPosition.y + y * textYIncrement;
                GameObject characterObject = Instantiate(characterPrefab);
                characterObject.transform.SetParent(textFactory.transform);
                characterObject.transform.position = new Vector3(xPos, yPos, 0f);
                characterObject.GetComponent<TMPro.TMP_Text>().text = c.ToString();
            }

            textFactoryCoroutine = null;

        }
    }
}
