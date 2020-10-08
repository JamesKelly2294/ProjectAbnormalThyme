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

    // Todo move to separate script
    public SpriteRenderer progressBarMeterFull;
    public SpriteRenderer progressBarProgress;

    private TrainManager trainManager;
    private TrackManager trackManager;

    StartTrackLight trackLight = StartTrackLight.Unknown;

    // Start is called before the first frame update
    void Start()
    {
        trainManager = GameObject.Find("TrainManager").GetComponent<TrainManager>();
        trackManager = FindObjectOfType<TrackManager>();

        progressBarProgress.enabled = false;
    }

    bool _timerTicking = false;
    float t = 0;

    // Update is called once per frame
    void Update()
    {
        if(!trainManager.IsIdleTrainWaiting())
        {
            t = 0;
            progressBarProgress.enabled = false;
            _timerTicking = false;
        } else if (trackManager.IsUpgradeApplied(TrackTypeUpgrade.TrainOMatic) && trainManager.IsIdleTrainWaiting() && !_timerTicking)
        {
            t = 0;
            _timerTicking = true;
            progressBarProgress.enabled = true;
        }

        if (_timerTicking)
        {
            t += Time.deltaTime;

            float pct = Mathf.Clamp01(t / trackManager.AutoTrainLauncherTime());
            //progressBarProgress.transform.position;
            progressBarProgress.transform.localScale = new Vector3(progressBarMeterFull.transform.localScale.x * pct, progressBarMeterFull.transform.localScale.y, progressBarMeterFull.transform.localScale.z);

            if (t >= trackManager.AutoTrainLauncherTime())
            {
                if (trainManager.SendNewTrain())
                {
                    t = 0;
                    progressBarProgress.enabled = false;
                    _timerTicking = false;
                }
            }
        }

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

    public void SendNewTrain()
    {
        trainManager.SendNewTrain();
    }
}
