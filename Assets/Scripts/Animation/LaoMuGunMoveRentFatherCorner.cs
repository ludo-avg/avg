using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaoMuGunMoveRentFatherCorner : MonoBehaviour
{
    //Setting
    Vector3 startPositon = new Vector3(-10.46f, -1.33f, 0f);
    Vector3 endPositon = new Vector3(0.79f, -1.33f, 0f);
    float moveTime = 3;
    float moveWaves = 6;
    float moveMaxY = 0.5f;

    Vector3 moveAwayEndPositon = new Vector3(10.52f, -1.33f, 0f);
    float moveAwayMoveTime = 2f;
    float moveAwayAngleSpeed = -360;

    //Cached
    float startTime = 0f;
    bool moveAway = false;
    Vector3 moveAwayStartPositon;
    float moveAwayStartTime;
    float moveAwayAngle;

    void OnEnable()
    {
        transform.position = startPositon;
        startTime = Time.time;
        moveAway = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (!moveAway)
        {
            float passedTime = Time.time - startTime;


            if (passedTime >= moveTime)
            {
                transform.position = endPositon;
                return;
            }

            float x = (endPositon.x - startPositon.x) / moveTime * passedTime;
            float y = 0f;
            {
                float timeWave = moveTime / moveWaves;
                float leftTime = passedTime % timeWave;
                float timeAngle = leftTime / timeWave * Mathf.PI;
                y = Mathf.Sin(timeAngle) * moveMaxY;
            }
            transform.position = startPositon + new Vector3(x, y, 0);
        }
        else
        {
            float passedTime = Time.time - moveAwayStartTime;
            transform.position = Vector3.Lerp(moveAwayStartPositon, moveAwayEndPositon, passedTime / moveAwayMoveTime);
            moveAwayAngle = moveAwayAngleSpeed * passedTime;
            transform.rotation = Quaternion.Euler(0, 0, moveAwayAngle);
        }
        
    }

    public void MoveAway()
    {
        moveAway = true;
        moveAwayStartPositon = transform.position;
        moveAwayStartTime = Time.time;
        moveAwayAngle = 0;
    }
}
