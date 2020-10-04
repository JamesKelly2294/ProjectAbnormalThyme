using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrainCar))]
public class TrainCarFront : MonoBehaviour
{
    public Train Train { get; protected set; }
    // Start is called before the first frame update
    void Start()
    {
        Train = GetComponent<TrainCar>().train;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
