using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PrintSiblingIndex : MonoBehaviour
{
    [Button("Print Sibling Index")]
    void Print()
    {
        print(transform.GetSiblingIndex());
    }
}
