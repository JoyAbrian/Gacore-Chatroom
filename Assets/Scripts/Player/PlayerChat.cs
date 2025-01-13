using TMPro;
using UnityEngine;
using Unity.Netcode;

public class PlayerChat : NetworkBehaviour
{
    [SerializeField] private GameObject chatBubblePrefab;
    [SerializeField] private Vector3 offset;

    public void SpawnChatBubble(string message)
    {
        if (!IsOwner) return;

        GameObject newChatBubble = Instantiate(chatBubblePrefab, gameObject.transform.position + offset, Quaternion.identity);
        newChatBubble.transform.SetParent(gameObject.transform);
        newChatBubble.transform.localPosition = offset;

        TMP_Text chatText = newChatBubble.GetComponentInChildren<TMP_Text>();
        if (chatText != null)
        {
            chatText.text = message;
        }

        //Destroy(newChatBubble, 5f);
    }
}