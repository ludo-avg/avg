using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public static DialogueBox singleton = null;
    private void Awake()
    {
        singleton = this;
    }
}
