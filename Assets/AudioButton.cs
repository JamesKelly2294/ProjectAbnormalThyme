using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public GameObject disabledImage;

    public void ToggleAudio()
    {
        disabledImage.SetActive(!AudioManager.main.ToggleSoundEffects());
        AudioManager.main.PlayButtonClick();
    }
}
