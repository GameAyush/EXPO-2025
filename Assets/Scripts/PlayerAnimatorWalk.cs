using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public float speedThreshold = 0.1f; // Minimum speed to trigger walking

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get Animator Component
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Calculate movement speed
        float speed = new Vector3(moveX, 0, moveZ).magnitude;

        // Update Animator's "Speed" parameter
        animator.SetFloat("Speed", speed);
    }
}
