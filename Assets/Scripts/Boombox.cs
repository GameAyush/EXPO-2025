using UnityEngine;

public class Boombox : MonoBehaviour
{
    private AudioSource musicSource; // Reference to the AudioSource

    void Start()
    {
        musicSource = GetComponent<AudioSource>(); // Get the AudioSource

        // Ensure the music starts playing at game start
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    void Update()
    {
        // Press "E" to toggle music on/off
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (musicSource.isPlaying)
            {
                musicSource.Pause(); // Pause music
            }
            else
            {
                musicSource.Play(); // Play music
            }
        }
    }
}
