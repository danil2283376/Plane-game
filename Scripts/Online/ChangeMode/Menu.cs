using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PvP()
    {
        // загрузить сцену с создание комнаты или подключения к комнате
        SceneManager.LoadScene("CreateRoomOrConnect");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
