using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCamera : MonoBehaviour
{
    Camera thisCamera = null;
    public RenderTexture renderTexture = null;
    void Start()
    {
        thisCamera.targetTexture = null;
    }

    void Update()
    {
        
    }
}
