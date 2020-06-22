using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Server : MonoBehaviour
{
    //TcpListener server;
    Socket server;

    IPHostEntry ipHost;
    IPAddress ipAddr;
    IPEndPoint iPEndPoint;

    List<Socket> clients;

    int quantityClients = 0;
    public bool closeServer = false;

    public Server(int port)
    {
        ipHost = Dns.GetHostEntry("localhost");
        ipAddr = ipHost.AddressList[0];
        iPEndPoint = new IPEndPoint(ipAddr, port);

        server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        clients = new List<Socket>();
    }

    public void ThreadListenServer()
    {
        Thread thread = new Thread(ListenServer);
        thread.Start();
    }

    public void ListenServer()
    {
        server.Bind(iPEndPoint);
        server.Listen(2);

        
        while (quantityClients <= 2)
        {
            Socket client = server.Accept();
            quantityClients++;
            clients.Add(client);
        }
    }

    public void Receive()
    {
        int steps = 0;
        while (/*!closeServer*/steps < 10)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                byte[] dataClients = new byte[256];
                int countData = clients[i].Receive(dataClients);
                print("Сервер сказал " + Encoding.UTF8.GetString(dataClients));
            }
        }


        if (closeServer)
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
