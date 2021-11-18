using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GetComponentTest : MonoBehaviour
{
    [Button("Com")]
    void Com()
    {
        print("GetComponent");
        var aa = GetComponent<BoxCollider>();
        print(aa.size.x);
        print(aa.size.y);
        print(aa.size.z);

        print("GetComponents");
        var bb = GetComponents<BoxCollider>();
        print("length: " + bb.Length);

        print("GetComponents,parent");
        var comPs = GetComponentsInParent<BoxCollider>();
        print("length: " + comPs.Length);
        foreach (var comP in comPs)
        {
            print($"x:{comP.size.x}, y:{comP.size.y}, z:{comP.size.z}");
        }

        print("GetComponents,children");
        var comCs = GetComponentsInChildren<BoxCollider>();
        print("length: " + comCs.Length);
        foreach (var comC in comCs)
        {
            print($"x:{comC.size.x}, y:{comC.size.y}, z:{comC.size.z}");
        }


    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
