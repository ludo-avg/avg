using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UserData : MonoBehaviour
{
    #region Singleton
    public static UserData singleton = null;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    public int health = 3;

    public int attack { get { return (chicken ? 3 : 0) + (knife ? 999 : 0); } }
    public int defence { get { return (cloth ? 1 : 0) + (dict ? 1 : 0); } }

    public bool steelWire = false;
    public bool chicken = false;
    public bool knife = false;
    public bool cover = false;
    public bool money = false;
    public bool cloth = false;
    public bool dict = false;


    #region Interaction1 - TimeChoice，Chalk
    public bool chalkOT = false;

    public void ChalkChooseLeft()
    {
        health -= 1;
    }
    public void ChalkChooseRight()
    {
        health -= 1;
    }
    public void ChalkOverTime()
    {
        chalkOT = true;
    }

    public bool Condition_IsChalkOverTime_R;
    public void Condition_IsChalkOverTime()
    {
        bool returnValue = chalkOT;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }

    public bool Condition_IsChalkNotOverTime_R;
    public void Condition_IsChalkNotOverTime()
    {
        bool returnValue = !chalkOT;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }
    #endregion

    #region Interaction2 - Choice，Rent

    public bool rentLeftChosen = false;

    public void RentInit()
    {
        rentLeftChosen = false;
    }
    public void RentChooseLeft()
    {
        rentLeftChosen = true;
        health -= 1;
    }
    public void RentChooseRight()
    {

    }


    public bool Condition_IsRentLeftChosen_R;
    public void Condition_IsRentLeftChosen()
    {
        bool returnValue = rentLeftChosen;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }

    public bool Condition_IsRentRightChosen_R;
    public void Condition_IsRentRightChosen()
    {
        bool returnValue = !rentLeftChosen;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }

    #endregion

    #region Function

    public void Beat()
    {
        health = health - (3 - defence);
    }

    #endregion


    #region End

    public bool Condition_IsKnife_R;
    public void Condition_IsKnife()
    {
        bool returnValue = knife;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }

    public bool Condition_IsChicken_R;
    public void Condition_IsChicken()
    {
        bool returnValue = chicken;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }

    public bool Condition_IsCover_R;
    public void Condition_IsCover()
    {
        bool returnValue = cover;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }

    public bool Condition_IsMoney_R;
    public void Condition_IsMoney()
    {
        bool returnValue = money;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }

    public bool Condition_IsNotHealth_R;
    public void Condition_IsNotHealth()
    {
        bool returnValue = health <= 0;

        {
            string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string returnName = methodName + "_R";
            Type myType = GetType();
            FieldInfo myFieldInfo = myType.GetField(returnName,
                BindingFlags.Public | BindingFlags.Instance);
            myFieldInfo.SetValue(this, returnValue);
        }
    }

    #endregion


    public void GetSteelWire()
    {
        steelWire = true;
    }
    public void GetChicken()
    {
        chicken = true;
    }

    public void GetKnife()
    {

        knife = true;
    }

    public void GetCover()
    {

        cover = true;
    }

    public void GetMoney()
    {

        money = true;
    }

    public void GetCloth()
    {
        cloth = true;
    }

    public void GetDict()
    {
        dict = true;
    }


}
