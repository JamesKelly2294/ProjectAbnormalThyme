using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    public GameObject disabledImage;

    public void ToggleMusic()
    {
        disabledImage.SetActive(!AudioManager.main.ToggleMusic());
        AudioManager.main.PlayButtonClick();
    }
}
