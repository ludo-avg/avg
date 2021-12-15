using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class Shit : MonoBehaviour
    {
        //inner setting
        float[] speeds = { 50, 100, 150, 200 };
        (float min, float max) angleRange = (-60, 60);
        float[] rotateSpeeds = { 270, 360, 540 };
        float lifeTime = 5f;

        //PreSetting
        float? speedSetting = null;
        float? angleSetting = null;
        float? rotateSpeedSetting = null;

        //runtime
        float speed;
        float angle;
        float rotateSpeed;

        public void PreStartSetting(float? speed = null, float? angle = null, float? rotateSpeed = null)
        {
            if (speed.HasValue)
            {
                speedSetting = speed;
            }
            if (angle.HasValue)
            {
                angleSetting = angle;
            }
            if (rotateSpeed.HasValue)
            {
                rotateSpeedSetting = rotateSpeed;
            }
        }

        void Start()
        {
            if (speedSetting.HasValue)
            {
                speed = speedSetting.Value;
            }
            else
            {
                speed = speeds[Random.Range(0, speeds.Length)];
            }

            if (angleSetting.HasValue)
            {
                angle = angleSetting.Value;
            }
            else
            {
                angle = Random.Range(angleRange.min, angleRange.max);
            }

            if (rotateSpeedSetting.HasValue)
            {
                rotateSpeed = rotateSpeedSetting.Value;
            }
            else
            {
                rotateSpeed = rotateSpeeds[Random.Range(0, rotateSpeeds.Length)];
            }

            StartCoroutine(C_Fly());
            StartCoroutine(C_LifeTime());
        }

        IEnumerator C_Fly()
        {
            while (true)
            {
                transform.localPosition += Quaternion.Euler(0, 0, angle) * new Vector3(0, speed * Time.fixedDeltaTime, 0);
                transform.localRotation *= Quaternion.Euler(0, 0, rotateSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
        }

        IEnumerator C_LifeTime()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}