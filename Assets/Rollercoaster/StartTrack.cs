using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartTrackLight
{
    Green = 0,
    Orange,
    Red
}

[RequireComponent(typeof(Track))]
public class StartTrack : MonoBehaviour
{
    public SpriteRenderer popupLightRenderer;
    public SpriteRenderer lightRenderer;
    public Sprite greenLightSprite;
    public Sprite orangeLightSprite;
    public Sprite redLightSprite;

    StartTrackLight trackLight = StartTrackLight.Green;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private float time = 0;
    void Update()
    {
        time += Time.deltaTime;

        if (time > 1.0f)
        {
            switch(trackLight)
            {
                case StartTrackLight.Green:
                    lightRenderer.sprite = orangeLightSprite;
                    trackLight = StartTrackLight.Orange;
                    popupLightRenderer.gameObject.SetActive(false);
                    break;
                case StartTrackLight.Orange:
                    lightRenderer.sprite = redLightSprite;
                    trackLight = StartTrackLight.Red;
                    break;
                case StartTrackLight.Red:
                    lightRenderer.sprite = greenLightSprite;
                    trackLight = StartTrackLight.Green;
                    popupLightRenderer.gameObject.SetActive(true);
                    break;
                default: break;
            }
            time = 0;
        }
    }
}
