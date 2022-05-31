using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class NoName
    {
        static public GameObject GetMouseHit()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = LayerMask.GetMask("Default");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask:layerMask))
            {
                if (hit.transform != null)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    return hitObject;
                }
                else
                    return null;
            }
            return null;
        }

        static public string GetMouseHitName()
        {
            GameObject obj = GetMouseHit();
            if (obj != null)
            {
                return obj.name;
            }
            return null;
        }

        static public GameObject GetMouseHitOfUIInScene()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = LayerMask.GetMask("UIInScene");

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask:layerMask))
            {
                if (hit.transform != null)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    return hitObject;
                }
                else
                    return null;
            }
            return null;
        }

        static public string GetMouseHitNameOfUIInScene()
        {
            GameObject obj = GetMouseHitOfUIInScene();
            if (obj != null)
            {
                return obj.name;
            }
            return null;
        }

        static public void SetAllChildrenInactive(Transform t)
        {
            foreach (Transform child in t)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}

