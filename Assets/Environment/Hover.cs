using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public Vector3 offsetFromStart;
    
    [Range(0, 5)]
    public float period;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = startPosition + new Vector3(
            offsetFromStart.x * Mathf.Sin(Time.time * (1 / period)), 
            offsetFromStart.y * Mathf.Sin(Time.time * (1 / period)), 
            offsetFromStart.z * Mathf.Sin(Time.time * (1 / period)));
    }
}
