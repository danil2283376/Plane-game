using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private void OnGUI()
    {
        const int width = 100;
        const int height = 85;

        Rect startGame = new Rect(Screen.width / 2, Screen.height / 2 + 50, width, height);
        Rect shop = new Rect(Screen.width / 2, Screen.height / 2 + 150, width, height);
        Rect exit = new Rect(Screen.width / 2, Screen.height / 2 + 250, width, height);

        if (GUI.Button(startGame, "Start"))
        {
            SceneManager.LoadScene("Stage1");
        }

        if (GUI.Button(shop, "Shop"))
        {
            //GameObject menu = GameObject.Find("Shop");
            //menu.SetActive(false);
            SceneManager.LoadScene("Shop");
        }

        if (GUI.Button(exit, "Exit"))
        {
            Application.Quit();
        }
    }

    private void Shop ()
    {

    }
}
