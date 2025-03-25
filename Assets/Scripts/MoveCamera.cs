using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CameraShake cameraShake;

    public Transform player; // Player to follow
    public float followSpeed = 5f; // Smooth following speed
    public float rotationSpeed = 3f; // Camera rotation speed
    public float zoomSpeed = 2f; // Speed of zooming
    public float minDistance = 2f; // Minimum zoom distance (prevents camera inside player)
    public float maxDistance = 8f; // Maximum zoom distance
    public float height = 2f; // Camera height above player
    public LayerMask collisionLayers; // Layers considered as obstacles (set in Inspector)

    private float currentDistance; // Current camera distance
    private float angleY = 0f; // Camera rotation angle

    void Start()
    {
        angleY = transform.eulerAngles.y; // Initialize camera rotation
        currentDistance = maxDistance; // Start at max zoom distance
        cameraShake = GetComponent<CameraShake>(); // Get CameraShake component
    }


    void Update()
    {
        // Rotate the camera using right mouse button
        if (Input.GetMouseButton(1))
        {
            angleY += Input.GetAxis("Mouse X") * rotationSpeed;
        }

        // Zoom in/out with mouse scroll (clamped)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }

    void LateUpdate()
    {
        // Compute desired camera position
        Vector3 desiredPosition = player.position - Quaternion.Euler(0, angleY, 0) * new Vector3(0, 0, currentDistance) + Vector3.up * height;

        // Perform a raycast to check for obstacles between player and camera
        RaycastHit hit;
        if (Physics.Raycast(player.position + Vector3.up * height, desiredPosition - player.position, out hit, maxDistance, collisionLayers))
        {
            transform.position = hit.point - (desiredPosition - player.position).normalized * 0.3f;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }

        // **Apply Camera Shake Offset**
        if (cameraShake != null)
        {
            transform.position += cameraShake.GetShakeOffset();
        }

        // Ensure the camera is always looking at the player
        transform.LookAt(player.position + Vector3.up * height * 0.5f);
    }

}
