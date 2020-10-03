using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RollerCoasterTrackType
{
    Loop = 0,
    Straight,
    Begin,
    End,
}

public class RollerCoasterTrack : MonoBehaviour
{
    public RollerCoasterTrackType type;

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
