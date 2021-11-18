using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools.Extensions;

public class UIState : MonoBehaviour
{
    GameObject health;
    GameObject statusPanel;
    TMP_Text statusButtonText;
    UnityEngine.UI.Text statusText;

    private void Start()
    {
        health = transform.LudoFind("Health", includeInactive: true).gameObject;
        statusPanel = transform.LudoFind("StatusPanel", includeInactive: true).gameObject;
        statusButtonText = transform.LudoFind("StatusButton", includeInactive: true).LudoFind("Text", includeInactive: true).GetComponent<TMP_Text>();
        statusText = statusPanel.transform.LudoFind("Text", includeInactive: true).GetComponent<UnityEngine.UI.Text>();
    }
    void Update()
    {
        var h1t = health.transform.LudoFind("Heart1t", includeInactive: true).gameObject;
        var h1f = health.transform.LudoFind("Heart1f", includeInactive: true).gameObject;
        var h2t = health.transform.LudoFind("Heart2t", includeInactive: true).gameObject;
        var h2f = health.transform.LudoFind("Heart2f", includeInactive: true).gameObject;
        var h3t = health.transform.LudoFind("Heart3t", includeInactive: true).gameObject;
        var h3f = health.transform.LudoFind("Heart3f", includeInactive: true).gameObject;

        int hp = UserData.singleton.health;
        if (hp < 1)
        {
            h1t.SetActive(false);
            h1f.SetActive(true);
        }
        else
        {
            h1t.SetActive(true);
            h1f.SetActive(false);
        }

        if (hp < 2)
        {
            h2t.SetActive(false);
            h2f.SetActive(true);
        }
        else
        {
            h2t.SetActive(true);
            h2f.SetActive(false);
        }

        if (hp < 3)
        {
            h3t.SetActive(false);
            h3f.SetActive(true);
        }
        else
        {
            h3t.SetActive(true);
            h3f.SetActive(false);
        }


        statusButtonText.text = statusPanel.activeSelf ? "隐藏\n状态" : "显示\n状态";
        string steelWire = UserData.singleton.steelWire ? "√" : "×";
        string chicken = UserData.singleton.chicken ? "√" : "×";
        string knife = UserData.singleton.knife ? "√" : "×";
        string cover = UserData.singleton.cover ? "√" : "×";
        string money = UserData.singleton.money ? "√" : "×";
        string cloth = UserData.singleton.cloth ? "√" : "×";
        string dict = UserData.singleton.dict ? "√" : "×";
        statusText.text = $"攻击：{UserData.singleton.attack}\n"
                        + $"防御：{UserData.singleton.defence}\n"
                        + "\n"
                        + $"铁丝：{steelWire}\n"
                        + $"鸡毛掸子：{chicken}\n"
                        + $"菜刀：{knife}\n"
                        + $"锅盖：{cover}\n"
                        + $"老爸的私房钱：{money}\n"
                        + $"棉衣棉裤：{cloth}\n"
                        + $"字典：{dict}\n";
    }

    public void StatusButtonClick()
    {
        statusPanel.SetActive(!statusPanel.activeSelf);
    }
}
