using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonShop : MonoBehaviour
{

    private void OnGUI()
    {
        const int width = 100;
        const int height = 85;

        Rect shop = new Rect(Screen.width / 2, Screen.height / 2 + 150, width, height);
        Rect exit = new Rect(0f, 0f, width, height);

        if (GUI.Button(shop, "Shop"))
        {
            GameObject menu = GameObject.Find("Menu");
            menu.SetActive(false);
        }

        if (GUI.Button(exit, "Exit"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
