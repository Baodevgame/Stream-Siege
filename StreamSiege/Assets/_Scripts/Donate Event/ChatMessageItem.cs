using UnityEngine;
using UnityEngine.UI;

public class ChatMessageItem : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 250f;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

        if (rect.anchoredPosition.x < -2500)
        {
            Destroy(gameObject);
        }
    }

    public void SetMessage(string user, string msg)
    {
        GetComponent<Text>().text = $"{user}: {msg}";
    }
}