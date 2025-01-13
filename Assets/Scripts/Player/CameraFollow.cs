using UnityEngine;
using Unity.Netcode;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private Transform target;

    void Start()
    {
        AssignLocalPlayerAsTarget();
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void AssignLocalPlayerAsTarget()
    {
        if (NetworkManager.Singleton.LocalClient != null && NetworkManager.Singleton.LocalClient.PlayerObject != null)
        {
            target = NetworkManager.Singleton.LocalClient.PlayerObject.transform;
        }
        else
        {
            Invoke(nameof(AssignLocalPlayerAsTarget), 0.5f);
        }
    }
}