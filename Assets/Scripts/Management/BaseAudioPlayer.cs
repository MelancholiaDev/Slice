using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAudioPlayer : MonoBehaviour
{
    private AudioSource sfxAudioSource;
    private void Awake()
    {
        sfxAudioSource = GetComponent<AudioSource>();
    }

    [SerializeField] private AudioClip[] allKnivesSound;
    public void PlayCutSound()
    {
        sfxAudioSource.PlayOneShot(allKnivesSound[Random.Range(0, allKnivesSound.Length)]);
    }

    [SerializeField] private AudioClip[] allJumpSounds;
    public void PlayJumpSound()
    {
        sfxAudioSource.PlayOneShot(allJumpSounds[Random.Range(0, allJumpSounds.Length)]);
    }
}
