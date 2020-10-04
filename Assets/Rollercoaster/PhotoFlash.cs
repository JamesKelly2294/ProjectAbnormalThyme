using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoFlash : MonoBehaviour
{
    public float firstFlashDuration;
    public float gapDuration;
    public float secondFlashDuration;
    public bool loop;
    public bool flashing;
    SpriteRenderer spriteRenderer;
    float _time;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!flashing) { return; }
        _time += Time.deltaTime;
        if((_time > 0.0f && _time < firstFlashDuration) || (_time > firstFlashDuration + gapDuration && _time < firstFlashDuration + gapDuration + secondFlashDuration))
        {
            spriteRenderer.enabled = true;
        } else
        {
            spriteRenderer.enabled = false;
        }

        if (_time > firstFlashDuration + gapDuration + secondFlashDuration + 0.5f)
        {
            if(loop)
            {
                ResetFlash();
            } else
            {
                flashing = false;
            }
        } 
    }

    public void ResetFlash()
    {
        _time = 0;
        flashing = true;
        spriteRenderer.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        TrainCarFront trainCar = other.gameObject.GetComponent<TrainCarFront>();
        if (trainCar != null)
        {
            ResetFlash();
        }
    }
}
