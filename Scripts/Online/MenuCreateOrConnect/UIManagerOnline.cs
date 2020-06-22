using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerOnline : MonoBehaviour
{
    public bool chat = false;
    public Animator chatAnimator;

    public void Chat()
    {
        chatAnimator.SetBool("Chat", chat);
        chat = !chat;
    }
    // создание потока для создания комнаты
    public void CreateRoomThread()
    {
        Thread thread = new Thread(CreateRoom);
        thread.Start();
    }
    // создание комнаты
    public void CreateRoom()
    {
        // создание комнаты повесить на компьютер сервер
        Server server = new Server(80);
        server.ThreadListenServer();
    }

    // создание потока с подключение к комнате
    public void ConnectRoomThread()
    {
        Thread thread = new Thread(ConnectRoom);
        thread.Start();
    }
    // соединение с комнатой
    public void ConnectRoom()
    {
        // подключится по IP и порту к комнате
        Client client = new Client(80);
        client.ThreadQuestion();
    }

    public void Exit()
    {
        SceneManager.LoadScene("OnlineMenu");
    }
}
