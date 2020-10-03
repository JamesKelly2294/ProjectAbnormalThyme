using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarBeginStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrainCar trainCar = other.gameObject.GetComponent<TrainCar>();
        if (trainCar != null)
        {
            Train train = trainCar.train;
            if(train.onTrack) { return; }
            train.onTrack = true;
        }
    }
}
