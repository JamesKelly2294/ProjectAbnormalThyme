using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMinSpeedReduction : MonoBehaviour
{
    public float speedReduction;

    // if false, sets to speed reduction
    public bool isMultiplier;

    public TrackTypeUpgrade requiredUpgrade;
    public ApplyMinSpeedReset minSpeedReset;
    TrackManager trackManager;

    private void Start()
    {
        trackManager = TrackManager.Instance;
        minSpeedReset.enabled = false;
    }

    float cachedMinSpeed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!trackManager.IsUpgradeApplied(requiredUpgrade)) {
            minSpeedReset.gameObject.SetActive(false);
            return;
        } else
        {
            minSpeedReset.gameObject.SetActive(true);
        }

        TrainCarFront trainCar = other.gameObject.GetComponent<TrainCarFront>();
        if (trainCar != null)
        {
            if (isMultiplier)
            {
                trainCar.Train.minimumSpeed *= speedReduction;
            }
            else
            {
                trainCar.Train.minimumSpeed = speedReduction;
            }
        }
    }
}
