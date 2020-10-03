using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{

    public AnimationCurve xAcceleration;
    public AnimationCurve yAcceleration;

    public float xAccelerationScale;
    public float yAccelerationScale;

    float t = 0;
    public float animationTime = 2;

    public float rotationRate = 100;

    public int value = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if ( t > animationTime) {
            t -= animationTime;
        }
 
        Vector3 offset = new Vector3(xAcceleration.Evaluate(t / animationTime) * xAccelerationScale, yAcceleration.Evaluate(t / animationTime) * yAccelerationScale, 0f);
        transform.localPosition += offset;

        transform.Rotate(new Vector3(0, 0, rotationRate * Time.deltaTime));
    }
}
