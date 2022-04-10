using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Tools.Extensions;

public class TeacherMove : MonoBehaviour
{
    //Setting
    float xLeft = -1.65f;
    float xRight = 1.65f;
    float y = 0.47f;
    float time = 2f;

    //cached
    Tween tween;
    GameObject teacher;
    GameObject teacherThrow;
    void Awake()
    {
        teacher = transform.LudoFind("Teacher", includeInactive: true).gameObject;
        teacherThrow = transform.LudoFind("TeacherThrow", includeInactive: true).gameObject;
    }

    void OnEnable()
    {
        teacher.SetActive(true);
        teacherThrow.SetActive(false);


        void NewMove()
        {
            //判断当前位置，离xLeft近还是xRight近。然后，向远的那个点移动。
            Vector3 left = new Vector3(xLeft, y, 0);
            Vector3 right = new Vector3(xRight, y, 0);
            Vector3 current = transform.position;
            Vector3 toLeft = current - left;
            Vector3 toRight = current - right;
            if (toLeft.magnitude < toRight.magnitude)
            {
                tween = DOTween.To(
                    () => transform.position,
                    value => transform.position = value,
                    new Vector3(xRight, y, 0),
                    time
                    )
                    .SetEase(Ease.InOutSine)
                    .OnComplete(NewMove);
            }
            else
            {
                tween = DOTween.To(
                    () => transform.position,
                    value => transform.position = value,
                    new Vector3(xLeft, y, 0),
                    time
                    )
                    .SetEase(Ease.InOutSine)
                    .OnComplete(NewMove);
            }
        }

        tween = DOTween.To(
            () => new Vector3(xLeft, y, 0),
            value => transform.position = value,
            new Vector3(xRight, y, 0),
            time
            )
            .SetEase(Ease.InOutSine)
            .OnComplete(NewMove);
    }

    void OnDisable()
    {
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }
    }

    public void ThrowChalk()
    {
        if(!gameObject.activeInHierarchy)
        {
            return;
        }

        teacher.SetActive(false);
        teacherThrow.SetActive(true);

        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }
    }
}
