using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint; // The last checkpoint the player touched
    public float respawnDelay = 1.5f; // Time before respawning
    public float fallLimit = -10f; // Y position threshold for falling

    void Start()
    {
        // Set initial respawn point to where the player starts
        respawnPoint = transform.position;
    }

    void Update()
    {
        // Check if player has fallen below the fall limit
        if (transform.position.y < fallLimit)
        {
            Debug.Log("Player fell off the map! Respawning...");
            Respawn();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        Debug.Log("Player died! Respawning...");
        yield return new WaitForSeconds(respawnDelay);

        // Restore player's position
        transform.position = respawnPoint;

        // Reset player's velocity (if using Rigidbody)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; // Stops falling motion
        }

        // Reset player's health
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth(); // Restore health
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position; // Update respawn location
            Debug.Log("Checkpoint Set at: " + respawnPoint);
        }

    }
}
