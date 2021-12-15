using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SecondCameraManager : MonoBehaviour
{
    [SerializeField] Camera secondCamera = null;

    [Button("Set To Initial")]
    public void SetToInitial()
    {
        secondCamera.gameObject.SetActive(false);
    }

    public void Awake()
    {
        secondCamera.gameObject.SetActive(true);
    }
}
