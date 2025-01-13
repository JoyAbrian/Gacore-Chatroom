using TMPro;
using UnityEngine;
using Unity.Netcode;

public class PlayerChat : NetworkBehaviour
{
    [SerializeField] private GameObject chatBubblePrefab;
    [SerializeField] private Vector3 offset;

    private GameObject currentChatBubble;

    public void SpawnChatBubble(string message)
    {
        if (!IsOwner) return;

        if (currentChatBubble == null)
        {
            currentChatBubble = Instantiate(chatBubblePrefab, transform.position + offset, Quaternion.identity);
            currentChatBubble.transform.SetParent(transform);
        }

        TMP_Text chatText = currentChatBubble.GetComponentInChildren<TMP_Text>();
        if (chatText != null)
        {
            chatText.text = message;
        }
    }

    void Update()
    {
        if (currentChatBubble != null)
        {
            currentChatBubble.transform.position = transform.position + offset;
        }
        DestroyChatBubble();
    }

    public void DestroyChatBubble()
    {
        if (currentChatBubble != null)
        {
            Destroy(currentChatBubble, 5f);
        }
    }
}