using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Collections.Concurrent;

public class DonateServer : MonoBehaviour
{
    public EnemyDonateManager enemyDonateManager;
    public PlayerDonateManager playerDonateManager;
    public ChatManager chatManager;

    private TcpListener server;
    private Thread serverThread;
    private bool running;

    private ConcurrentQueue<string> queue = new ConcurrentQueue<string>();

    private void Start()
    {
        running = true;
        serverThread = new Thread(ServerLoop);
        serverThread.Start();
    }

    private void ServerLoop()
    {
        server = new TcpListener(IPAddress.Any,7777);
        server.Start();

        Debug.Log("Server Started");

        while (running)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int len = stream.Read(buffer, 0, buffer.Length);

            string msg = Encoding.UTF8.GetString(buffer, 0, len);

            queue.Enqueue(msg);

            client.Close();
        }
    }

    private void Update()
    {
        while (queue.TryDequeue(out string msg))
        {
            Handle(msg);
        }
    }

    private void Handle(string msg)
    {
        if (msg.StartsWith("CHAT|"))
        {
            string[] data = msg.Split('|');

            string user = data[1];
            string message = data[2];

            chatManager.AddMessage(user, message);

            return;
        }
        switch (msg)
        {
            // Enemy Donate
            case "DONATE_ENEMY_1":
                enemyDonateManager.AddDonate(1);
                break;

            case "DONATE_ENEMY_20":
                enemyDonateManager.AddDonate(20);
                break;

            case "DONATE_ENEMY_100":
                enemyDonateManager.AddDonate(100);
                break;

            case "DONATE_ENEMY_999":
                enemyDonateManager.AddDonate(999);
                break;

            // Player Donate
            case "DONATE_PLAYER_1":
                playerDonateManager.AddDonate(1);
                break;

            case "DONATE_PLAYER_20":
                playerDonateManager.AddDonate(20);
                break;

            case "DONATE_PLAYER_100":
                playerDonateManager.AddDonate(100);
                break;

            case "DONATE_PLAYER_999":
                playerDonateManager.AddDonate(999);
                break;
        }
    }

    private void OnApplicationQuit()
    {
        running = false;
        server?.Stop();
        serverThread?.Abort();
    }
}