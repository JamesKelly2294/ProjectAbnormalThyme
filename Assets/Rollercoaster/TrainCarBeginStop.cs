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

    private HashSet<Train> _affectedTrains = new HashSet<Train>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrainCar trainCar = other.gameObject.GetComponent<TrainCar>();
        if (trainCar != null)
        {
            Train train = trainCar.train;
            if (_affectedTrains.Contains(train)) { return; }
            _affectedTrains.Add(train);
            train.brakingPower = 10;
            train.isBrakingFullStop = true;
        }
    }
}
