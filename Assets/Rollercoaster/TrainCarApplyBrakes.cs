using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarApplyBrakes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Range(0, 100)]
    public float brakingPower = 5.0f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        TrainCarFront trainCar = other.gameObject.GetComponent<TrainCarFront>();
        if (trainCar != null)
        {
            trainCar.Train.brakingPower = brakingPower;
        }
    }
}
