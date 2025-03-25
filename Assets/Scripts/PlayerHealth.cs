using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityEditor.Rendering.LookDev;

public class PlayerHealth : MonoBehaviour
{
    private CameraShake cameraShake;
    public int maxHealth = 100;  // Maximum health
    private int currentHealth;   // Player's current health
    public HealthBarUI healthBar; // Reference to the fancy health bar UI
    public float invincibilityTime = 1f; // Time where player can't take damage
    public float blinkInterval = 0.1f; // Time between blinks
    private bool isInvincible = false; // Prevents multiple hits
    private SpriteRenderer spriteRenderer; // For blinking effect

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.UpdateHealth(currentHealth, maxHealth);

        cameraShake = Camera.main.GetComponent<CameraShake>(); // Get CameraShake script
    }
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.UpdateHealth(currentHealth, maxHealth);

        if (cameraShake != null)
        {
            cameraShake.ShakeScreen(); // Trigger camera shake
        }

        StartCoroutine(BlinkEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    IEnumerator BlinkEffect()
    {
        isInvincible = true; // Prevent multiple hits
        for (float i = 0; i < invincibilityTime; i += blinkInterval)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
            yield return new WaitForSeconds(blinkInterval);
        }
        spriteRenderer.enabled = true; // Ensure visibility is restored
        isInvincible = false;
    }
    void Die()
    {
        Debug.Log("Player Died! Respawning...");
        GetComponent<PlayerRespawn>().Respawn(); // Call respawn function instead of disabling the player
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth; // Restore full health
        healthBar.UpdateHealth(currentHealth, maxHealth); // Update UI
        Debug.Log("Health Reset: " + currentHealth);
    }

}
