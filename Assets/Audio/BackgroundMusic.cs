using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Other,
    Music,
    Ambiance,
    SoundEffect,
}

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip audioClip;
    public float volumeScale = 1.0f;
    public AudioType type;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.volume = volumeScale;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        Destroy(audioSource);
    }
}
