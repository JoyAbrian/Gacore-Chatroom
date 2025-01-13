using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class InputChat : NetworkBehaviour
{
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private Button sendBtn;

    private Transform target;
    private PlayerChat playerChat;

    private bool isChatActive = false;

    void Start()
    {
        chatPanel.SetActive(false);
        sendBtn.onClick.AddListener(SendChatMessage);

        AssignLocalPlayerAsTarget();
    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isChatActive)
            {
                if (!string.IsNullOrWhiteSpace(inputText.text))
                {
                    SendChatMessage();
                }
                else
                {
                    chatPanel.SetActive(false);
                    isChatActive = false;
                }
            }
            else
            {
                isChatActive = true;
                chatPanel.SetActive(true);
                inputText.Select();
                inputText.ActivateInputField();
            }
        }
    }

    public void SendChatMessage()
    {
        string message = inputText.text;
        inputText.text = string.Empty;

        if (playerChat != null)
        {
            playerChat.SpawnChatBubble(message);
        }

        chatPanel.SetActive(false);
        isChatActive = false;
    }

    private void AssignLocalPlayerAsTarget()
    {
        if (NetworkManager.Singleton.LocalClient != null && NetworkManager.Singleton.LocalClient.PlayerObject != null)
        {
            target = NetworkManager.Singleton.LocalClient.PlayerObject.transform;

            playerChat = target.GetComponent<PlayerChat>();

            if (playerChat == null)
            {
                Debug.LogError("PlayerChat component not found on the local player object!");
            }
        }
        else
        {
            Invoke(nameof(AssignLocalPlayerAsTarget), 0.5f);
        }
    }
}