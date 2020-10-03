using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackType
{
    Loop = 0,
    Straight,
    Start,
    End,
}

public class Track : MonoBehaviour
{
    public TrackType type;

    // Start is called before the first frame update
    void Start()
    {
        TrackPathManager.Instance.RegisterTrack(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
