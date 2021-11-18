using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SetAllChildrenInactive : MonoBehaviour
{
    [Button("所有子节点 不可见")]
    public void SetAllInactive()
    {
        Tools.NoName.SetAllChildrenInactive(transform);
    }
}
