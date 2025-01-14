using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UsernameManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro nameDisplay;

    void Start()
    {
        PlayerData localPlayer = FindLocalPlayer();
        if (localPlayer != null)
        {
            localPlayer.Username.OnValueChanged += (oldValue, newValue) =>
            {
                nameDisplay.text = newValue;
            };
            nameDisplay.text = localPlayer.Username.Value;
        }
        else
        {
            Debug.LogError("Local player not found!");
        }
    }

    private PlayerData FindLocalPlayer()
    {
        if (NetworkManager.Singleton.LocalClient != null && NetworkManager.Singleton.LocalClient.PlayerObject != null)
        {
            return NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerData>();
        }
        else
        {
            Invoke(nameof(FindLocalPlayer), 0.5f);
            return null;
        }
    }
}