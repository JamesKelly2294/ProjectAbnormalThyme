using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    // todo, calculate world space bounds of camera and use that for respawning clouds
    // see  https://answers.unity.com/questions/501893/calculating-2d-camera-bounds.html

    public float MinX = -4.5f;
    public float MaxX = 4.5f;
    public float Speed = 0.25f;
    public float VerticalVariance = 0.01f;

    private Vector3 startingPosition;
    private float randomSeed;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        randomSeed = Random.Range(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (Time.deltaTime * Speed), startingPosition.y + Mathf.Sin(Time.time + randomSeed) * VerticalVariance, transform.position.z);

        if (transform.position.x > MaxX)
        {
            transform.position = new Vector3(MinX, transform.position.y, transform.position.z);
        }
    }
}
