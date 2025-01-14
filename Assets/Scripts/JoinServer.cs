using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinServer : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;

    public void JoiningServer(string status)
    {
        if (!string.IsNullOrEmpty(usernameInput.text))
        {
            GlobalVariables.username = usernameInput.text;
            GlobalVariables.playerStatus = status;
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.LogError("Username cannot be empty!");
        }
    }
}