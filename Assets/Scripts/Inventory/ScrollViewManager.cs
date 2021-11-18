#define Test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace Inventory
{
    public class ScrollViewManager : MonoBehaviour
    {
        #region
        public static ScrollViewManager singleton;
        private void Awake()
        {
            singleton = this;
        }
        #endregion

        public GameObject prefab;

        void Start()
        {
            for (int i = 0; i < Inventory.singleton.minBoxSize; i++)
            {
                Instantiate(prefab, transform);
            }

            transform.parent.parent.gameObject.SetActive(false);
        }

        public void AddBox(int count)
        {
            for (int i = 1; i<= count; i++)
            {
                Instantiate(prefab, transform);
            }
        }

        public void RemoveBox(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                var child = transform.GetChild(transform.childCount - i);
                Destroy(child.gameObject);
            }
        }

        public void SetBox(int index, Item item)
        {
            transform.GetChild(index).GetComponent<InventoryBox>().SetItem(item);
        }

        public void SetGlow(int index, bool isGlow)
        {
            transform.GetChild(index).GetComponent<InventoryBox>().SetGlow(isGlow);
        }



#if Test
        private int go = 1;
        [Button("go")]
        void Go()
        {
            GameObject newObj = Instantiate(prefab, transform);
            newObj.GetComponent<Image>().color = Random.ColorHSV();
            newObj.name = "New" + go;
            go++;
            newObj.GetComponentInChildren<Text>().text = newObj.name;
        }

        [Button("GetChild")]
        void GetChild()
        {
            for(int i=0; i<transform.childCount; i++)
            {
                print(transform.GetChild(i).name);
            }
        }
#endif
    }
}