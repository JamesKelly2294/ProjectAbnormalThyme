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

    static Vector3[] localLoopWaypoints = new[] { new Vector3(-0.45f, -0.3367424f, 0f), new Vector3(-0.3128065f, -0.340954f, 0f), new Vector3(-0.2208815f, -0.3195973f, 0f), new Vector3(-0.05060089f, -0.2290567f, 0f), new Vector3(0.1157227f, -0.09050867f, 0f), new Vector3(0.2168044f, 0.05065752f, 0f), new Vector3(0.2486197f, 0.2162478f, 0f), new Vector3(0.1872462f, 0.3406396f, 0f), new Vector3(0.07577837f, 0.429786f, 0f), new Vector3(-0.0949018f, 0.4404377f, 0f), new Vector3(-0.2326232f, 0.3434279f, 0f), new Vector3(-0.2872765f, 0.249081f, 0f), new Vector3(-0.2900648f, 0.1055469f, 0f), new Vector3(-0.1779549f, -0.0652881f, 0f), new Vector3(0.00666678f, -0.240903f, 0f), new Vector3(0.229753f, -0.3267424f, 0f), new Vector3(0.45f, -0.3367424f, 0f) };
    static Vector3[] localStraightWaypoints = new[] { new Vector3(-0.45f, -0.3367424f, 0f), new Vector3(0.0f, -0.3367424f, 0f) };
    static Vector3[] localStartWaypoints = new[] { new Vector3(-0.9204822f, -0.7832438f, 0f), new Vector3(-0.2055083f, -0.487436f, 0f), new Vector3(0.02655159f, -0.400973f, 0f), new Vector3(0.154705f, -0.3587843f, 0f), new Vector3(0.2481711f, -0.3392277f, 0f), new Vector3(0.3811634f, -0.3367424f, 0f), new Vector3(0.435487f, -0.3367424f, 0f), new Vector3(0.4833426f, -0.3367424f, 0f) };
    static Vector3[] localEndWaypoints = new[] { new Vector3(-0.4812305f, -0.3421845f, 0f), new Vector3(-0.2873616f, -0.3393334f, 0f), new Vector3(-0.1562149f, -0.3678436f, 0f), new Vector3(0.482412f, -0.6016266f, 0f), new Vector3(1.172357f, -0.8639196f, 0f) };

    // Start is called before the first frame update
    void Start()
    {
        TrackPathManager.Instance.RegisterTrack(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3[] LocalWaypoints()
    {
        switch (type)
        {
            case TrackType.Loop:
                return localLoopWaypoints;
            case TrackType.Straight:
                return localStraightWaypoints;
            case TrackType.Start:
                return localStartWaypoints;
            case TrackType.End:
                return localEndWaypoints;
            default:
                return localStraightWaypoints;
        }
    }
}
