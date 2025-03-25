using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarUI : MonoBehaviour
{
    public RectTransform healthFill; // Green bar (current health)
    public RectTransform damageFill; // Gray bar (lost health)
    public Image hitFlash; // Red flash effect when taking damage
    public float damageSpeed = 0.5f; // Speed for damage animation
    private float maxWidth; // Stores the original width of the health bar

    void Start()
    {
        maxWidth = healthFill.rect.width; // Get full bar width at start
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        float newWidth = Mathf.Clamp(healthPercentage * maxWidth, 0f, maxWidth);

        // Instead of shrinking from both sides, move the RIGHT edge only
        healthFill.sizeDelta = new Vector2(newWidth, healthFill.sizeDelta.y);
        healthFill.anchoredPosition = new Vector2(0, healthFill.anchoredPosition.y); // Keep left side fixed

        StartCoroutine(FlashRed());
        StartCoroutine(SmoothDamageReduction(newWidth));
    }


    IEnumerator FlashRed()
    {
        hitFlash.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f); // Flash duration
        hitFlash.gameObject.SetActive(false);
    }

    IEnumerator SmoothDamageReduction(float newWidth)
    {
        yield return new WaitForSeconds(0.2f); // Delay before reducing damage fill

        float currentWidth = damageFill.sizeDelta.x;
        float elapsedTime = 0;

        while (elapsedTime < damageSpeed)
        {
            elapsedTime += Time.deltaTime;
            float smoothWidth = Mathf.Lerp(currentWidth, newWidth, elapsedTime / damageSpeed);
            damageFill.sizeDelta = new Vector2(smoothWidth, damageFill.sizeDelta.y);
            yield return null;
        }

        damageFill.sizeDelta = new Vector2(newWidth, damageFill.sizeDelta.y);
    }
}
