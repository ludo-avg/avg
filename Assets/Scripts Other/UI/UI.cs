using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject settingPanel = null;
    [SerializeField] GameObject returnToStartMenuPanel = null;
    [SerializeField] [Scene] string startMenuScene = null;

    private void Start()
    {
        settingPanel.SetActive(false);
    }

    public void ReturnToStartMenuButton()
    {
        returnToStartMenuPanel.SetActive(true);
    }

    public void ReturnToStartMenuPanelConfirm()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(startMenuScene);
    }

    public void ReturnToStartMenuPanelCancel()
    {
        returnToStartMenuPanel.SetActive(false);
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
