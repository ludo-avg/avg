using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;
using Tools.Extensions;
using Random = UnityEngine.Random;

namespace StartMenu
{
    public class StartMenu : MonoBehaviour
    {
        //manully setting
        public GameObject scrollViewContent = null;
        [Scene] public string gameScene = null;

        public AudioSource bgm = null;

        public AudioSource laoMuGenSource = null;
        public AudioSource aWenSource = null;
        public AudioClip laoMuGenClip = null;
        public AudioClip aWenClip = null;

        public GameObject laoMuGenImage;
        public GameObject aWenImage;
        public GameObject aWenLightning;

        public ClipSet laoMuGenFartClips;
        public ClipSet aWenFartClips;

        //cached
        [NonSerialized]public Image blackForeground;


        private void Start()
        {
            IEnumerator DanceCoroutine()
            {
                IEnumerator LaoMuGenCoroutine(int i, float[] timeInvervalParameter)
                {
                    if (i % 2 == 0)
                    {
                        laoMuGenImage.transform.localRotation = Quaternion.Euler(0, 0, 20f);
                    }
                    else
                    {
                        laoMuGenImage.transform.localRotation = Quaternion.Euler(0, 0, -20f);
                    }

                    if (i == timeInvervalParameter.Length - 1)
                    {
                        ShitLaucher.singleton.LaoMuGenLauch(speed:200, null, null);
                    }

                    yield return null;
                }

                IEnumerator AWenCoroutine(int i, float[] timeInvervalParameter)
                {
                    yield return new WaitForSeconds(0.05f);

                    if (i != timeInvervalParameter.Length - 1)
                    {
                        aWenImage.transform.localScale = new Vector3(-aWenImage.transform.localScale.x,
                                                    aWenImage.transform.localScale.y,
                                                    aWenImage.transform.localScale.z);
                    }
                    if (i == timeInvervalParameter.Length - 1)
                    {
                        aWenLightning.SetActive(true);
                    }
                    if (i == 0)
                    {
                        aWenLightning.SetActive(false);
                    }

                }


                bgm.PlayDelayed(0.25f);

                float loopLength = 4f;
                float loopCount = 0;
                float[] timeInverval = { 0f, 0.5f, 0.5f, 0.5f, 0.5f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f };
                float[] time = new float[11];
                for (int i = 0; i < 11; i++)
                {
                    if (i == 0)
                    {
                        time[i] = timeInverval[i];
                    }
                    else time[i] = time[i - 1] + timeInverval[i];
                }
                bool[] played = Enumerable.Repeat(false, 11).ToArray();
                float start = Time.time;

                while (true)
                {
                    float current = Time.time - start;
                    int currentloopCount = Mathf.FloorToInt(current / loopLength);
                    if (currentloopCount > loopCount)
                    {
                        loopCount = currentloopCount;
                        played = Enumerable.Repeat(false, 11).ToArray();
                    }
                    float currentLeft = current - loopCount * loopLength;

                    for (int i = 0; i < 11; i++)
                    {
                        if (currentLeft >= time[i] && !played[i])
                        {
                            played[i] = true;

                            //LaoMuGen
                            StartCoroutine(LaoMuGenCoroutine(i, timeInverval));
                            //AWen
                            StartCoroutine(AWenCoroutine(i, timeInverval));
                        }
                    }

                    yield return null;
                }
            }

            blackForeground = transform.LudoFind("BlackForeground").GetComponent<Image>();
            blackForeground.color = new Color(0, 0, 0, 0);
            StartCoroutine(DanceCoroutine());

        }

        public void LeftClick()
        {
            float newX = GetBoundry(scrollViewContent.transform.localPosition.x + 1).left;
            float y = scrollViewContent.transform.localPosition.y;
            scrollViewContent.transform.localPosition =
                new Vector3(newX, y, 0);

            Sound.singleton.sfxAS.PlayOneShot(Sound.singleton.clickClip);
        }

        public void RightClick()
        {
            float newX = GetBoundry(scrollViewContent.transform.localPosition.x - 1).right;
            float y = scrollViewContent.transform.localPosition.y;
            scrollViewContent.transform.localPosition =
                new Vector3(newX, y, 0);

            Sound.singleton.sfxAS.PlayOneShot(Sound.singleton.clickClip);
        }

        public void EnterGameScene()
        {
            float time = 1f;
            IEnumerator C_EnterScene()
            {
                yield return new WaitForSeconds(time);
                UnityEngine.SceneManagement.SceneManager.LoadScene(gameScene);
            }

            blackForeground.DOColor(new Color(0,0,0,1), time);
            Sound.singleton.BgmAndVoiceDown(time);
            Sound.singleton.sfxAS.PlayOneShot(Sound.singleton.transitionClip);
            StartCoroutine(C_EnterScene());
            
        }

        public void ClickLaoMuGen()
        {
            if (!laoMuGenSource.isPlaying)
            {
                laoMuGenSource.clip = laoMuGenClip;
                laoMuGenSource.Play();
            }

            ShitLaucher.singleton.LaoMuGenLauch();

            Sound.singleton.sfxAS.PlayOneShot(laoMuGenFartClips.clips[Random.Range(0, laoMuGenFartClips.clips.Length)]);
        }

        public void ClickAWen()
        {
            if (!aWenSource.isPlaying)
            {
                aWenSource.clip = aWenClip;
                aWenSource.Play();
            }
            ShitLaucher.singleton.AWenLauch();

            Sound.singleton.sfxAS.PlayOneShot(aWenFartClips.clips[Random.Range(0, aWenFartClips.clips.Length)]);
        }

        (float left, float right) GetBoundry(float pos)
        {
            float constAdd = 240 * 3;
            pos = pos + constAdd;

            float right = pos - pos % 240;
            float left = pos + 240;

            left -= constAdd;
            right -= constAdd;

            return (left, right);
        }
    }
}