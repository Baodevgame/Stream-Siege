using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [SerializeField]
    private ChatMessageItem messagePrefab;

    [SerializeField]
    private RectTransform container;

    public void AddMessage(string user, string msg)
    {
        ChatMessageItem item = Instantiate(messagePrefab, container);

        RectTransform rect = item.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(1200,Random.Range(-100f, 100f));

        item.SetMessage(user, msg);
    }
}