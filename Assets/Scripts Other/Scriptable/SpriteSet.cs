using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteSet", menuName = "ScriptableObjects/SpriteSet", order = 1)]
public class SpriteSet : ScriptableObject
{
    public Sprite[] images;
}
