using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndEffect : MonoBehaviour
{
    public TeacherMove teacherMove;
    public ChalkAnimation chalkAnimation;
    public void TeacherThrowChalk()
    {
        teacherMove.ThrowChalk();
        chalkAnimation.ThrowChalk();
    }

    public void StopChalk()
    {
        chalkAnimation.StopChalk();
    }
}
