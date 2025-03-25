using UnityEngine;

public class FootstepLoop : MonoBehaviour
{
    public AudioSource footstepAudio; // Footstep Audio Source

    void Start()
    {
        if (footstepAudio == null)
        {
            footstepAudio = GetComponent<AudioSource>(); // Auto-assign AudioSource
        }
    }

    void Update()
    {
        // Check if any movement key is being held
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow);

        if (isMoving)
        {
            if (!footstepAudio.isPlaying) // Ensure the sound starts if not playing
            {
                footstepAudio.Play();
            }
            else
            {
                footstepAudio.UnPause(); // Resume if paused
            }
        }
        else
        {
            if (footstepAudio.isPlaying) // Pause instead of stopping
            {
                footstepAudio.Pause();
            }
        }
    }
}
