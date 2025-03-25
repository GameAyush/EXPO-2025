using UnityEngine;

public class Landmine : MonoBehaviour
{
    public int damage = 25; // Damage amount
    public GameObject explosionPrefab; // Explosion effect prefab
    public AudioClip explosionSound; // Explosion sound effect
    private AudioSource audioSource; // Audio source for sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply damage to player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Play explosion sound
            if (explosionSound != null && audioSource != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            // Create explosion effect
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Destroy the landmine
            Destroy(gameObject);
        }
    }
}
