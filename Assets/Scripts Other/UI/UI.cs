using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject settingPanel = null;

    private void Start()
    {
        settingPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleSettingPanel()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
    }
}
