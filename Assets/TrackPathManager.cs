using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using System.Linq;

public class TrackPathManager : MonoBehaviour
{
    static TrackPathManager _instance;

    public static TrackPathManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<TrackPathManager>();
            }
            return _instance;
        }
    }

    public GameObject target;
    private Vector3 targetPosition;

    Vector3[] localLoopWaypoints = new[] { new Vector3(-0.45f, -0.3367424f, 0f), new Vector3(-0.3128065f, -0.340954f, 0f), new Vector3(-0.2208815f, -0.3195973f, 0f), new Vector3(-0.05060089f, -0.2290567f, 0f), new Vector3(0.1157227f, -0.09050867f, 0f), new Vector3(0.2168044f, 0.05065752f, 0f), new Vector3(0.2486197f, 0.2162478f, 0f), new Vector3(0.1872462f, 0.3406396f, 0f), new Vector3(0.07577837f, 0.429786f, 0f), new Vector3(-0.0949018f, 0.4404377f, 0f), new Vector3(-0.2326232f, 0.3434279f, 0f), new Vector3(-0.2872765f, 0.249081f, 0f), new Vector3(-0.2900648f, 0.1055469f, 0f), new Vector3(-0.1779549f, -0.0652881f, 0f), new Vector3(0.00666678f, -0.240903f, 0f), new Vector3(0.229753f, -0.3267424f, 0f), new Vector3(0.45f, -0.3367424f, 0f) };
    Vector3[] localStraightWaypoints = new[] { new Vector3(-0.45f, -0.3367424f, 0f), new Vector3(0.45f, -0.3367424f, 0f) };

    public List<RollerCoasterTrack> tracks;
    
    private void Awake()
    {
        _instance = this;
        tracks = new List<RollerCoasterTrack>();
        targetPosition = target.transform.position;
    }

    public void RegisterTrack(RollerCoasterTrack track)
    {
        if (!tracks.Contains(track))
        {
            tracks.Add(track);
            tracks.Sort((a, b) => (a.transform.position.x.CompareTo(b.transform.position.x)));
        }
    }

    public void DemoAnimation()
    {
        target.transform.position = targetPosition;

        IEnumerable<Vector3> waypoints = new List<Vector3>();
        foreach (RollerCoasterTrack track in tracks) {
            // Todo: this is crazy fucking slow my dudes lol
            List<Vector3> localWaypoints;
            switch (track.type)
            {
                case RollerCoasterTrackType.Loop:
                    localWaypoints = new List<Vector3>(localLoopWaypoints);
                    break;
                case RollerCoasterTrackType.Straight:
                    localWaypoints = new List<Vector3>(localStraightWaypoints);
                    break;
                default:
                    localWaypoints = new List<Vector3>(localStraightWaypoints);
                    break;
            }

            List<Vector3> newWaypoints = new List<Vector3>(localWaypoints.Select(v => v + track.transform.position));
            waypoints = waypoints.Concat(newWaypoints);
        }

        const float speed = 1.5f;
        var result = target.transform.DOPath(waypoints.ToArray(), speed * tracks.Count(), PathType.CatmullRom, PathMode.Sidescroller2D);
        result.SetLookAt(.01f, true);
        result.SetEase(Ease.InOutQuad);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
