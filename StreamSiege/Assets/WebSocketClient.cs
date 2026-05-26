using UnityEngine;
using NativeWebSocket;

public class WebSocketClient : MonoBehaviour
{
    WebSocket websocket;

    public LiveEventManager eventManager;

    async void Start()
    {
        websocket = new WebSocket("ws://localhost:8080");

        websocket.OnOpen += () =>
        {
            Debug.Log("? Connected to server");
        };

        websocket.OnMessage += (bytes) =>
        {
            string msg = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("?? Received: " + msg);

            HandleMessage(msg);
        };

        await websocket.Connect();
    }

    void HandleMessage(string json)
    {
        GiftData data = JsonUtility.FromJson<GiftData>(json);

        if (data.type == "gift")
        {
            eventManager.OnEnemyDonate(data.amount);
        }
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}

[System.Serializable]
public class GiftData
{
    public string type;
    public int amount;
}