using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Tools.Extensions;

public class ChalkAnimation : MonoBehaviour
{
    //Setting
    Vector3 chalkMiddleStartPosition = new Vector3(0, 1.2f, 0);
    Vector3 chalkMiddleEndPosition = new Vector3(0, -1.3f, 0);

    //Cached
    GameObject chalkMiddle;
    GameObject chalkLeft;
    GameObject chalkRight;
    Tween tween = null;
    Coroutine coroutine = null;
    void Start()
    {
        chalkMiddle = transform.LudoFind("ChalkMiddle").gameObject;
        chalkLeft = transform.LudoFind("ChalkLeft").gameObject;
        chalkRight = transform.LudoFind("ChalkRight").gameObject;
        chalkMiddle.SetActive(false);
        chalkLeft.SetActive(false);
        chalkRight.SetActive(false);
    }

    public void ThrowChalk()
    {
        chalkMiddle.SetActive(true);
        tween = DOTween.To(
            () => chalkMiddleStartPosition,
            value => chalkMiddle.transform.position = value,
            chalkMiddleEndPosition,
            0.3f
            ).
            OnComplete(StartChalkFlash);
    }

    void StartChalkFlash()
    {
        tween = null;
        chalkMiddle.SetActive(false);
        coroutine = StartCoroutine(ChalkFlashCoroutine());
    }

    IEnumerator ChalkFlashCoroutine( )
    {
        chalkLeft.SetActive(true);
        chalkRight.SetActive(false);

        while (true)
        {
            yield return 0.05f;
            chalkLeft.SetActive(!chalkLeft.activeSelf);
            chalkRight.SetActive(!chalkRight.activeSelf);
        }
    }

    public void StopChalk()
    {
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        chalkMiddle.SetActive(false);
        chalkLeft.SetActive(false);
        chalkRight.SetActive(false);
    }
}
