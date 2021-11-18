using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object1 : MonoBehaviour
{
    public GameObject obj2;
    void Awake()
    {
        Debug.Log($"1.Awake() was called.  Frame:{Time.frameCount}");
        
    }

    void OnEnable()
    {
        Debug.Log($"1.OnEnable() was called.  Frame:{Time.frameCount}");
    }

    void Start()
    {
        Debug.Log($"1.Start() was called.  Frame:{Time.frameCount}");
        
        
    }

    void Update()
    {
        Debug.Log($"1.Update() was called. Frame:{Time.frameCount}");

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space pressed.");
            obj2.SetActive(true);
        }
        Debug.Log("1.Update() after space pressed.");
    }
}
