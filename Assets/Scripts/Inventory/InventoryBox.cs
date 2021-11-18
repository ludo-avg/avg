using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryBox : MonoBehaviour
    {
        public Image itemImage;
        public GameObject glow;
        private void Start()
        {
            
        }

        public void SetItem(Item inItem)
        {
            if (inItem == null)
            {
                itemImage.sprite = null;
                itemImage.gameObject.SetActive(false);
            }
            else
            {
                itemImage.sprite = inItem.image.sprite;
                itemImage.gameObject.SetActive(true);
            }
        }

        public void SetGlow(bool isGlow)
        {
            glow.SetActive(isGlow);
        }

        public void ButtonClick()
        {
            int index = transform.GetSiblingIndex();
            Inventory.singleton.Click(index);
        }



    }
}