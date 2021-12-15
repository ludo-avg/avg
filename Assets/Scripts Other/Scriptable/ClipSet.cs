using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipSet", menuName = "ScriptableObjects/ClipSet", order = 2)]
public class ClipSet : ScriptableObject
{
    public AudioClip[] clips;
}
