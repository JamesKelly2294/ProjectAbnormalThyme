using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float speed = 2.0f;
    public float minDistance = 0;
    public float maxDistance = 9.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(Mathf.Min(speed * Time.deltaTime + transform.position.x, maxDistance), transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(Mathf.Max(-(speed * Time.deltaTime) + transform.position.x, minDistance), transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO(james) - calculate midpoint of traintrack, center camera there?
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
}
