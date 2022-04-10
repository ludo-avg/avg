using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndEffect : MonoBehaviour
{
    public TeacherMove teacherMove;
    public ChalkAnimation chalkAnimation;
    public LaoMuGunMove laoMuGunMove;
    public LaoMuGunMoveRentFatherCorner laoMuGunMoveRentFatherCorner;
    public void TeacherThrowChalk()
    {
        teacherMove.ThrowChalk();
        chalkAnimation.ThrowChalk();
    }

    public void StopChalk()
    {
        chalkAnimation.StopChalk();
    }

    public void LaoMuGunMove()
    {
        laoMuGunMove.Move();
    }

    public void LaoMuGunRentFatherCornerMoveAway()
    {
        laoMuGunMoveRentFatherCorner.MoveAway();
    }
}
