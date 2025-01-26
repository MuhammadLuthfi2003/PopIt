using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance;

    private AudioSource audioSource;

    [Header("Gameplay SFX")]
    public AudioClip explosionSFX;
    public AudioClip bubblePopSFX;
    
    public AudioClip speedSFX;
    public AudioClip stunSFX;
    public AudioClip swingSFX;

    [Header("Bubble SFX")]
    public AudioClip[] bubbleSpawnSFXs;

    [Header("UI SFX")]
    public AudioClip buttonClickSFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayExplosionSFX()
    {
        audioSource.PlayOneShot(explosionSFX);
    }

    public void PlayBubblePopSFX()
    {
        audioSource.PlayOneShot(bubblePopSFX);
    }

    public void PlayBubbleSpawnSFX()
    {
        int randomIndex = Random.Range(0, bubbleSpawnSFXs.Length - 1);
        audioSource.PlayOneShot(bubbleSpawnSFXs[randomIndex]);
    }

    public void PlaySpeedSFX()
    {
        audioSource.PlayOneShot(speedSFX);
    }

    public void PlayStunSFX()
    {
        audioSource.PlayOneShot(stunSFX);
    }

    public void PlayButtonClickSFX()
    {
        audioSource.PlayOneShot(buttonClickSFX);
    }

    public void PlaySwingSFX()
    {
        audioSource.PlayOneShot(swingSFX);
    }
}
