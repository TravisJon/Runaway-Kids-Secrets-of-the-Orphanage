using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(footstepSound);
        }
    }

    // Panggil metode ini dari animasi atau skrip pergerakan pemain
    public void OnPlayerStep()
    {
        PlayFootstepSound();
    }
}
