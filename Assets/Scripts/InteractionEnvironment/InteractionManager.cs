//C#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//Unity
using UnityEngine;
using UnityEngine.EventSystems;
//Plugins
using DG.Tweening;
using TMPro;
//Project
using Interactions;
using Modules;

public class InteractionManager : MonoBehaviour
{
    #region Singleton
    public static InteractionManager singleton = null;
    private void Awake()
    {
        singleton = this;
    }
    #endregion 

    //Setting
    [SerializeField] private GameObject startBackground = null;

    //runtime
    List<InteractionBase> list;
    [NonSerialized] public InteractionBase current = null;

    private void Start()
    {
        Tools.NoName.SetAllChildrenInactive(InteractionList.singleton.transform);
        Tools.NoName.SetAllChildrenInactive(InteractionData.singleton.transform);
        BackgroundManager.singleton.SetStartBackground(startBackground);

        list = new List<InteractionBase>();
        foreach (Transform child in InteractionList.singleton.transform)
        {
            list.Add(child.GetComponent<InteractionBase>());
        }
        list = list.OrderBy(o => o.transform.GetSiblingIndex()).ToList();


        //需要等待其他组件就位，再Start，所以晚一帧开始。
        StartCoroutine(LateStart());
    }


    /// <summary>
    /// 需要等待其他组件就位，再Start，所以晚一帧开始。
    /// </summary>
    /// <returns></returns>
    IEnumerator LateStart()
    {
        yield return null;
        if (list.Count > 0)
        {
            current = list[0];
        }
        else
        {
            Debug.LogWarning("Interaction List empty.");
        }
        current.AStart();
    }

    private void Update()
    {
        if (current == null) return;
        /*
            先Interact
            然后Update
            最后判End
        */
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            current.AInteract();
        }
        
        current.AUpdate();
        if (current.ended == true)
        {
            var oldCurrent = current;
            current = GetCurrentNext();

            if (current == null)
            {
                current = oldCurrent;
                Debug.LogWarning("Trying to find current+1， but current is already end.");
            }
            else
            {
                current.AStart();
                //
                //print($"next is {current.name}, index: {current.transform.GetSiblingIndex()}");

            }
        }
    }

    /// <summary>
    /// return null, if invalid current
    /// </summary>
    /// <returns></returns>
    private InteractionBase GetCurrentNext()
    {
        bool somethingIsNull = false;
        InteractionBase nextFound = null;

        var currentGotos = current.@goto;
        

        if (currentGotos.Length == 0)
        {
            somethingIsNull = true;
        }
        else
        {
            bool found = false;
            foreach(var oneGoto in currentGotos)
            {
                var condition = oneGoto.condition;

                bool conditionTrue = false;
                if (oneGoto.condition == null)
                {
                    conditionTrue = true;
                }
                else
                {
                    if (condition.GetPersistentEventCount() == 0)
                    {
                        conditionTrue = true;
                    }
                    else
                    {
                        condition.Invoke();
                        string conditionMethod = condition.GetPersistentMethodName(0);
                        string valueName = conditionMethod + "_R";
                        bool conditionValue = false;

                        Type myType = UserData.singleton.GetType();
                        FieldInfo myFieldInfo = myType.GetField(valueName,
                            BindingFlags.Public | BindingFlags.Instance);
                        conditionValue = (bool)myFieldInfo.GetValue(UserData.singleton);

                        if (conditionValue)
                        {
                            conditionTrue = true;
                        }
                    }
                }

                if (conditionTrue)
                {
                    nextFound = oneGoto.interaction;
                }

                if (conditionTrue)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                somethingIsNull = true;
            }
        }

        if (somethingIsNull || (!somethingIsNull && nextFound == null))
        {
            int next = current.transform.GetSiblingIndex() + 1;
            if (next < list.Count)
            {
                return list[next];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return nextFound;   
        }
    }
}
