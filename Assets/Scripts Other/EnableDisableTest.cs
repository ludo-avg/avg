using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableTest : MonoBehaviour
{
    private void OnEnable()
    {
        print("OnEnable");
    }

    private void OnDisable()
    {
        print("OnDisable");
    }
}
