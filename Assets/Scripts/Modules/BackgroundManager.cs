using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Extensions;

namespace Modules
{
    public class BackgroundManager : MonoBehaviour
    {
        #region
        public enum Type
        {
            Simple = 0,
            Complex = 1
        }
        #endregion
        static public BackgroundManager singleton;

        [NonSerialized] public SpriteRenderer foregroundSR;
        [NonSerialized] public GameObject currentBackground;
        [NonSerialized] public Type currentType;
        [NonSerialized] public Coroutine currentCoroutine;
        [NonSerialized] public Action currentFinishFunc;
        public void Awake()
        {
            singleton = this;

            //初始化
            foregroundSR = transform.LudoFind("Foreground", includeInactive: true).GetComponent<SpriteRenderer>();
            currentBackground = null;
            currentType = Type.Simple;
            currentCoroutine = null;
            currentFinishFunc = null;

            //隐藏子节点
            foregroundSR.gameObject.SetActive(false);
            transform.LudoFind("BackgroundExample", includeInactive: true).gameObject.SetActive(false);
        }

        public void SetStartBackground(GameObject startBackground)
        {
            if (startBackground == null)
            {
                throw new Exception("startBackground should not be null.");
            }
            else
            {
                startBackground.SetActive(true);
            }
            ;
            currentBackground = startBackground;
            if (startBackground.GetComponent<SpriteRenderer>() != null)
            {
                currentType = Type.Simple;
            }
            else
            {
                currentType = Type.Complex;
            }
        }
    }
}