using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tools.Extensions
{
    public static class Extension
    {
        public static Transform LudoFind(this Transform t, string n, bool includeInactive = false, bool recursive = false)
        {
            if (!recursive && !includeInactive)
            {
                return t.Find(n);
            }

            if (!recursive && includeInactive)
            {
                Transform found = null;
                foreach (Transform tt in t)
                {
                    if (tt.name == n)
                    {
                        found = tt;
                    }
                }
                return found;
            }

            //recursive
            Transform[] ts = t.GetComponentsInChildren<Transform>(includeInactive);
            foreach (Transform tt in ts)
            {
                if (tt.name == n)
                {
                    return tt;
                }
            }
            return null;
        }
    }
}

