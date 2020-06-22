using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    public Animator extensionMenu;

    //settings
    public Animator extensionSettings;

    private bool click = false;

    public void Resume()
    {
        extensionMenu.SetBool("MenuOn", false);
    }
    //НАСТРОЙКИ НЕ РАБОТАЮТ!
    public void Settings()
    {
        print("АНИМАЦИЯ НАСТРОЕК НЕ РАБОТАЕТ");
        extensionSettings.SetBool("Settings", true);
    }

    public void SettingsExit()
    {
        extensionSettings.SetBool("Settings", false);
    }

    public void Menu()
    {
        extensionMenu.SetBool("MenuOn", true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
        {
            click = !click;
            if (click)
            {
                Menu();
            }
            else
            {
                Resume();
            }
        }
    }
}
