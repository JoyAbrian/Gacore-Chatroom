using UnityEngine;
using Unity.Netcode;

public class StartGame : MonoBehaviour
{
    public void Start()
    {
        if (GlobalVariables.playerStatus == "host")
        {
            NetworkManager.Singleton.StartHost();
        }
        else if (GlobalVariables.playerStatus == "client")
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}