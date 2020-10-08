using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("GUI")]
    public AudioClip buttonClickSuccess;
    public AudioClip buttonClickFailure;

    [Header("Trains")]
    public AudioClip trainLaunch;
    public List<AudioClip> money;

    [Header("Ambiance")]
    public AudioClip parkAmbiance;
    
    [Header("Music")]
    public AudioClip musicTrack;

    public static AudioManager main { get; private set; }

    private AudioSource mainAudioSource;
    private AudioSource musicAudioSource;
    private AudioSource ambientAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        mainAudioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        mainAudioSource.playOnAwake = false;

        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource.clip = musicTrack;
        musicAudioSource.volume = 0.3f;
        musicAudioSource.loop = true;
        musicAudioSource.Play();

        ambientAudioSource = gameObject.AddComponent<AudioSource>();
        ambientAudioSource.clip = parkAmbiance;
        ambientAudioSource.volume = 0.11f;
        ambientAudioSource.loop = true;
        ambientAudioSource.Play();
    }

    private bool isMusicEnabled = true;
    public bool ToggleMusic()
    {
        isMusicEnabled = !isMusicEnabled;
        if (!isMusicEnabled)
        {
            musicAudioSource.volume = 0;
        }
        else
        {
            musicAudioSource.volume = 0.3f;
        }
        return isMusicEnabled;
    }

    private bool isSoundEffectEnabled = true;
    public bool ToggleSoundEffects()
    {
        isSoundEffectEnabled = !isSoundEffectEnabled;
        if(!isSoundEffectEnabled)
        {
            ambientAudioSource.volume = 0;
            mainAudioSource.volume = 0;
        }
        else
        {
            ambientAudioSource.volume = 0.11f;
            mainAudioSource.volume = 1.0f;
        }
        return isSoundEffectEnabled;
    }

    // Absurd money audio management
    int moneyEffectCurrentCount = 0;
    const int moneyEffectLimit = 20;
    const int bufferSize = 60;
    bool[] rollingBuffer = new bool[bufferSize];
    int currentFrame = 0;
    bool moneyEffectPlayedThisFrame;

    private void FixedUpdate()
    {
        if (rollingBuffer[currentFrame])
        {
            moneyEffectCurrentCount -= 1;
        }

        if(moneyEffectPlayedThisFrame)
        {
            moneyEffectCurrentCount += 1;
        }

        rollingBuffer[currentFrame] = moneyEffectPlayedThisFrame;
        currentFrame = (currentFrame + 1) % bufferSize;

        moneyEffectPlayedThisFrame = false;
    }

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
    {
        mainAudioSource.pitch = 1.0f;
        mainAudioSource.volume =isSoundEffectEnabled ? volumeScale : 0.0f;
        mainAudioSource.PlayOneShot(clip, volumeScale);
    }

    public void PlayButtonClick()
    {
        mainAudioSource.pitch = Random.Range(0.85f, 1.25f);
        mainAudioSource.PlayOneShot(buttonClickSuccess, Random.Range(0.9f, 1.1f));
    }

    public void PlayButtonClickFailure()
    {
        mainAudioSource.pitch = Random.Range(0.85f, 1.25f);
        mainAudioSource.PlayOneShot(buttonClickFailure, Random.Range(0.9f, 1.1f));
    }

    public void PlayMoneyEffect()
    {
        if (moneyEffectPlayedThisFrame) { return; }

        float pct = moneyEffectCurrentCount / (float)moneyEffectLimit;
        if(pct > 1) { return; }
        else if (Random.Range(0, 1) > pct) { return; }
        moneyEffectPlayedThisFrame = true;
        mainAudioSource.pitch = Random.Range(0.85f, 1.25f);
        mainAudioSource.PlayOneShot(money[Random.Range(0, money.Count-1)], Random.Range(0.75f, 0.75f) * Mathf.Min(1-pct, 0.40f));
    }
}
