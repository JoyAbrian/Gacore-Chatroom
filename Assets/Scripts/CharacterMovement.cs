using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    private Animator anim;
    private Rigidbody rb;
    private Vector3 movementDirection;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        if (!IsOwner)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        AnimatePlayer();
    }

    void FixedUpdate()
    {
        if (movementDirection.magnitude >= 0.1f)
        {
            Vector3 targetPosition = rb.position + movementDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void AnimatePlayer()
    {
        bool isWalking = movementDirection.magnitude >= 0.1f;
        anim.SetBool("isWalking", isWalking);
    }
}