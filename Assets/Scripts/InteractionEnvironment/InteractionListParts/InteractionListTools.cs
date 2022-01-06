//C#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//Unity
using UnityEngine;
using UnityEngine.Events;
//Plugin
using NaughtyAttributes;
//Project
using Interactions;
using Tools.Extensions;
//Editor
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

public partial class InteractionList : MonoBehaviour
{
    private void PutLast(GameObject child)
    {
        child.transform.parent = gameObject.transform;
        child.transform.SetAsLastSibling();
    }

    private void PutLastAndRename(GameObject child, string name = null)
    {
        child.transform.parent = gameObject.transform;
        child.transform.SetAsLastSibling();

        if (name != null)
        {
            child.name = name;
        }
        else
        {
            if (child.GetComponent<Idle>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Idle>() != null)
                    {
                        num++;
                    }
                }
                child.name = "Idle" + num;
            }

            if (child.GetComponent<GameEnd>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<GameEnd>() != null)
                    {
                        num++;
                    }
                }
                child.name = "GameEnd" + num;
            }

            if (child.GetComponent<Dialogue>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Dialogue>() != null)
                    {
                        num++;
                    }
                }
                child.name = "Dialogue" + num;
            }

            if (child.GetComponent<Character>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Character>() != null)
                    {
                        num++;
                    }
                }
                child.name = "Character" + num;
            }

            if (child.GetComponent<ChangeBackground>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<ChangeBackground>() != null)
                    {
                        num++;
                    }
                }
                child.name = "ChangeBackground" + num;
            }

            if (child.GetComponent<Choice>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<Choice>() != null)
                    {
                        num++;
                    }
                }
                child.name = "Choice" + num;
            }

            if (child.GetComponent<TimeChoice>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<TimeChoice>() != null)
                    {
                        num++;
                    }
                }
                child.name = "TimeChoice" + num;
            }

            if (child.GetComponent<PointAndClick>() != null)
            {
                int num = 0;
                foreach (Transform c in transform)
                {
                    if (c.GetComponent<PointAndClick>() != null)
                    {
                        num++;
                    }
                }
                child.name = "CustomInteraction" + num;
            }
        }
    }
}
