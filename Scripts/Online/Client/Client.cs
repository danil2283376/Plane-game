using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Client : MonoBehaviour
{
    Socket client;
    IPHostEntry server;
    IPAddress ip;
    IPEndPoint ipServer;

    public Client(int port)
    {
        server = Dns.GetHostEntry("localhost");
        ip = server.AddressList[0];
        ipServer = new IPEndPoint(ip, port);

        client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(ipServer);
    }

    public void ThreadQuestion()
    {
        Thread thread = new Thread(Question);
        thread.Start();
    }

    public void Question()
    {
        int steps = 0;
        while (true)
        {
            string questionClient = "ХУЙ СОСИ" + steps++;
            //print("Клиент сказал: " + questionClient);
            byte[] question = Encoding.UTF8.GetBytes(questionClient);
            client.Send(question);

            if (steps == 10)
            {
                break;
            }
        }
        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }
}
