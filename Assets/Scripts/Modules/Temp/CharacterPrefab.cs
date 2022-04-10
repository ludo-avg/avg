using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CharacterPrefab : MonoBehaviour
{
    //Setting
    float moveDistance = 0.6f;
    float time = 2.5f;


    //Cached
    Tween tweenMove;
    Tween tweenColor;
    void Start()
    {
        Destroy(this.gameObject, time);

        Vector3 dir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, 2f), 0).normalized;
        if (UnityEngine.Random.Range(0,100) <= 20)
        {
            dir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, -1f), 0).normalized;
        }
        dir = dir * UnityEngine.Random.Range(0.4f, 1f);

        tweenMove = DOTween.To(
            () => transform.position,
            value => transform.position = value,
            transform.position + dir * moveDistance,
            time
            );
        TMP_Text text = GetComponent<TMP_Text>();
        tweenColor = DOTween.To(
            () => text.color,
            value => text.color = value,
            new Color(text.color.r, text.color.g, text.color.b, 0.2f),
            time
            );
    }

    private void OnDestroy()
    {
        tweenMove.Kill();
        tweenColor.Kill();
    }
}
