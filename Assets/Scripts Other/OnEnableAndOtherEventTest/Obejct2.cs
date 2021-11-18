using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obejct2 : MonoBehaviour
{
    void Awake()
    {
        Debug.Log($"2.Awake() was called.  Frame:{Time.frameCount}");
    }

    void OnEnable()
    {
        Debug.Log($"2.OnEnable() was called.  Frame:{Time.frameCount}");
    }

    void Start()
    {
        Debug.Log($"2.Start() was called.  Frame:{Time.frameCount}");
    }

    void Update()
    {
        Debug.Log($"2.Update() was called.  Frame:{Time.frameCount}");
    }
}
