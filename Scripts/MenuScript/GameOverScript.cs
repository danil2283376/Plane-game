using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private void OnGUI()
    {
        const int width = 100;
        const int height = 100;

        Rect replay = new Rect(Screen.width / 2, Screen.height / 2, width, height);
        Rect mainMenu = new Rect(Screen.width / 2, Screen.height / 2 + 150, width, height);


        if (GUI.Button(replay, "Replay"))
        {
            SceneManager.LoadScene("Stage1");
        }

        if (GUI.Button(mainMenu, "Main menu"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
