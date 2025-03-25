using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f; // Duration of shake
    public float shakeMagnitude = 0.2f; // Intensity of shake
    private Vector3 shakeOffset = Vector3.zero; // Shake offset
    private float shakeTime = 0f; // Time remaining for shake

    void Update()
    {
        if (shakeTime > 0)
        {
            // Generate small random offsets
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float zOffset = Random.Range(-1f, 1f) * shakeMagnitude;

            shakeOffset = new Vector3(xOffset, yOffset, zOffset);
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero; // Reset shake effect
        }
    }

    public Vector3 GetShakeOffset()
    {
        return shakeOffset; // Returns the current shake offset
    }

    public void ShakeScreen()
    {
        shakeTime = shakeDuration; // Start shaking
    }
}
