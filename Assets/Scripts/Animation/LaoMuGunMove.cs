using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaoMuGunMove : MonoBehaviour
{
    //Setting
    Vector3 startPositon = new Vector3(-11.2f, -2.09f, 0f);
    Vector3 endPositon = new Vector3(-2.03f, -2.09f, 0f);
    float moveTime = 3;
    float moveWaves = 6;
    float moveMaxY = 0.5f;

    //Cached
    float startTime = 0f;
    bool move = false;
    void OnEnable()
    {
        transform.position = startPositon;
        startTime = 0;
        move = false;
    }

    void Update()
    {
        if(!move)
        {
            return;
        }

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

    public void Move()
    {
        startTime = Time.time;
        move = true;
    }
}
