using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        #region Singleton
        public static Inventory singleton;
        private void Awake()
        {
            singleton = this;
        }

        #endregion


        [System.NonSerialized]public int minBoxSize = 4;
        public List<Item> itemList;

        void Start()
        {
            itemList = new List<Item>();
        }

        void Update()
        {

        }
        

        private int selectIndex = -1;

        public void AddItem(Item item)
        {
            itemList.Add(item);
            RefreshScrollView();
        }

        public void RemoveItem(int index)
        {
            itemList.RemoveAt(index);

            //change glow
            if (selectIndex == index)
            {
                ScrollViewManager.singleton.SetGlow(selectIndex, false);
                selectIndex = -1;
            }
            //change item
            RefreshScrollView();
        }

        public void Click(int index)
        {
            return;

            int preSelectIndex = selectIndex;

            if (GetItem(index) != null)
            {
                if (selectIndex == index)
                {
                    selectIndex = -1;
                }
                else
                {
                    selectIndex = index;
                }
            }

            //set glow
            if (preSelectIndex != selectIndex)
            {
                if (preSelectIndex >= 0)
                {
                    ScrollViewManager.singleton.SetGlow(preSelectIndex, false);
                }
                if (selectIndex >= 0)
                {
                    ScrollViewManager.singleton.SetGlow(selectIndex, true);
                }
            }
            
        }


        public Item GetSelectedItem()
        {
            if (selectIndex >= 0)
            {
                return itemList[selectIndex];
            }
            return null;
        }

        public bool HasItem(Item item)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i] == item)
                {
                    return true;
                }
            }
            return false;
        }

        public void Use()
        {
            Item item = itemList[selectIndex];
            item.durability -= 1;

            if (item.durability == 0)
            {
                RemoveItem(selectIndex);
            }
        }

        public void Use(Item item)
        {
            
            item.durability -= 1;
            
            if (item.durability == 0)
            {
                int index = -1;
                for(int i = 0; i < itemList.Count; i++)
                {
                    if(itemList[i] == item)
                    {
                        index = i;
                    }
                }
                if (index >= 0) RemoveItem(index);
            }
        }


        public Item GetItem(int index)
        {
            if (index < itemList.Count)
            {
                return itemList[index];
            }
            return null;
        }

        private void RefreshScrollView()
        {
            //先设置数量
            //再设置内容

            //数量
            {
                if (itemList.Count > minBoxSize)
                {
                    int currentBoxSize = ScrollViewManager.singleton.transform.childCount;
                    if (currentBoxSize < itemList.Count)
                    {
                        ScrollViewManager.singleton.AddBox(itemList.Count - currentBoxSize);
                    }
                    if (currentBoxSize > itemList.Count)
                    {
                        ScrollViewManager.singleton.RemoveBox(currentBoxSize - itemList.Count);
                    }
                }

                if (itemList.Count <= minBoxSize)
                {
                    int currentBoxSize = ScrollViewManager.singleton.transform.childCount;
                    if (ScrollViewManager.singleton.transform.childCount != minBoxSize)
                    {
                        ScrollViewManager.singleton.RemoveBox(currentBoxSize - minBoxSize);
                    }
                }
            }
            //内容
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    ScrollViewManager.singleton.SetBox(i, itemList[i]);
                }
                if (itemList.Count < minBoxSize)
                {
                    for (int i = itemList.Count; i < minBoxSize; i++)
                    {
                        ScrollViewManager.singleton.SetBox(i, null);
                    }
                }
            }
            

        }


    }
}