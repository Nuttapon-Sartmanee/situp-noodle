using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySoundManager_script : MonoBehaviour
{
    public AudioClip noodle1_sound , noodle2_sound , noodle3_sound , noodle4_sound , noodleEnd_sound , hit1 , hit2 , hit3;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void playRandomSound(float volume)
    {
        AudioClip[] noodleSounds = { noodle1_sound, noodle2_sound, noodle3_sound, noodle4_sound };

        if (noodleSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, noodleSounds.Length);
            AudioClip randomSound = noodleSounds[randomIndex];

            if (randomSound != null)
            {
                audioSource.volume = volume;
                audioSource.clip = randomSound;
                audioSource.Play();
            }
            else
            {
                Debug.LogError("AudioClip is null.");
            }
        }
        else
        {
            Debug.LogError("No AudioClips provided.");
        }
    }

    public void playRandomHitSound(float volume)
    {
        AudioClip[] hitSounds = { hit1, hit2, hit3 };

        if (hitSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            AudioClip randomSound = hitSounds[randomIndex];

            if (randomSound != null)
            {
                audioSource.volume = volume;
                audioSource.clip = randomSound;
                audioSource.Play();
            }
            else
            {
                Debug.LogError("AudioClip is null.");
            }
        }
        else
        {
            Debug.LogError("No AudioClips provided.");
        }
    }

    public void playStartSound(float volume)
    {
        audioSource.volume = volume;
        audioSource.clip = noodle1_sound;
        audioSource.Play();
    }

    public void playEndSound(float volume)
    {
        audioSource.volume = volume;
        audioSource.clip = noodleEnd_sound;
        audioSource.Play();
    }
}
