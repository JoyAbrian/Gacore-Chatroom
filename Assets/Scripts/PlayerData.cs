using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    public NetworkVariable<string> Username = new NetworkVariable<string>();
    public NetworkVariable<string> GameMode = new NetworkVariable<string>();

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Username.Value = GlobalVariables.username;
            GameMode.Value = GlobalVariables.playerStatus;
        }

        Username.OnValueChanged += OnUsernameChanged;
    }

    private void OnDestroy()
    {
        Username.OnValueChanged -= OnUsernameChanged;
    }

    private void OnUsernameChanged(string oldValue, string newValue)
    {
        Debug.Log($"Username changed from {oldValue} to {newValue}");
    }
}