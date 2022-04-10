﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class Item : MonoBehaviour
    {
        public string itemName;
        public string acquireText;
        public AudioClip auquireAudio;
        public UnityEngine.UI.Image image;
        public int durability;
    }
}
