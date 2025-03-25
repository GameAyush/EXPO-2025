using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float rotationSpeed = 10f; // Rotation speed
    public Transform cameraTransform; // Assign the camera in Unity Inspector

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody component
        rb.freezeRotation = true; // Prevent unwanted rotation
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Smooth movement
    }

    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow

        // Get camera-based forward & right vectors (ignore vertical tilt)
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // Movement direction relative to camera
        moveDirection = (camForward * moveZ + camRight * moveX).normalized;

        // Rotate player to face movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Apply smooth movement using MovePosition instead of velocity
        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
