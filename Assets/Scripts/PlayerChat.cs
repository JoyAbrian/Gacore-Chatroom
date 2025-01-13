using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChat : MonoBehaviour
{
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private Button sendBtn;

    private bool isChatActive = false;

    void Start()
    {
        chatPanel.SetActive(false);
        sendBtn.onClick.AddListener(SendChatMessage);
    }

    void Update()
    {
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
        Debug.Log($"Sending message: {message}");

        chatPanel.SetActive(false);
        isChatActive = false;
    }
}