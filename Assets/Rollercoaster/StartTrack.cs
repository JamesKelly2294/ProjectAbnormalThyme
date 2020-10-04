using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartTrackLight
{
    Unknown = -1,
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

    private TrainManager trainManager;

    StartTrackLight trackLight = StartTrackLight.Unknown;

    // Start is called before the first frame update
    void Start()
    {
        trainManager = GameObject.Find("TrainManager").GetComponent<TrainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StartTrackLight newLightState;
        if (!trainManager.CanTrackAcceptNewTrain() || !trainManager.IsIdleTrainWaiting())
        {
            newLightState = StartTrackLight.Orange;
        } else if (trainManager.IsTrackFull())
        {
            newLightState = StartTrackLight.Red;
        } else
        {
            newLightState = StartTrackLight.Green;
        }

        if (newLightState != trackLight)
        {
            trackLight = newLightState;
            switch (trackLight)
            {
                case StartTrackLight.Green:
                    lightRenderer.sprite = greenLightSprite;
                    popupLightRenderer.gameObject.SetActive(true);
                    break;
                case StartTrackLight.Orange:
                    lightRenderer.sprite = orangeLightSprite;
                    popupLightRenderer.gameObject.SetActive(false);
                    break;
                case StartTrackLight.Red:
                    lightRenderer.sprite = redLightSprite;
                    popupLightRenderer.gameObject.SetActive(false);
                    break;
                default:
                    lightRenderer.sprite = null;
                    popupLightRenderer.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
