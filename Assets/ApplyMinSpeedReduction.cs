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
        trackManager = FindObjectOfType<TrackManager>();
    }

    float cachedMinSpeed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!trackManager.IsUpgradeApplied(requiredUpgrade)) { minSpeedReset.enabled = false; return; }

        TrainCarFront trainCar = other.gameObject.GetComponent<TrainCarFront>();
        if (trainCar != null)
        {
            cachedMinSpeed = trainCar.Train.minimumSpeed;
            minSpeedReset.resetSpeed = cachedMinSpeed;
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
