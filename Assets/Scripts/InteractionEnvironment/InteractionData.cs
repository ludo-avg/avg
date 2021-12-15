using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionData : MonoBehaviour
{
    #region Singleton
    public static InteractionData singleton = null;
    void Awake()
    {
        singleton = this;
    }
    #endregion
}
