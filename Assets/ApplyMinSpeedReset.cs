using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMinSpeedReset : MonoBehaviour
{
    public float resetSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrainCarFront trainCar = other.gameObject.GetComponent<TrainCarFront>();
        if (trainCar != null)
        {
            trainCar.Train.minimumSpeed = resetSpeed;
        }
    }
}
