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

    public List<GameObject> targets;
    private List<Vector3> targetPositions;

    public List<Track> tracks;
    
    private void Awake()
    {
        _instance = this;
        tracks = new List<Track>();
        targetPositions = new List<Vector3>();
        foreach (GameObject go in targets)
        {
            targetPositions.Add(go.transform.position);
        }
    }

    public void RegisterTrack(Track track)
    {
        if (!tracks.Contains(track))
        {
            tracks.Add(track);
            tracks.Sort((a, b) => (a.transform.position.x.CompareTo(b.transform.position.x)));
        }
    }

    public void DemoAnimation()
    {
        if(!Application.isPlaying) { return; }
        for (int i = 0; i < targets.Count(); i++)
        {
            GameObject target = targets[i];
            target.transform.position = targetPositions[i];
        }

        List<Vector3> waypoints = new List<Vector3>();
        foreach (Track track in tracks) {
            // Todo: this is crazy fucking slow my dudes lol
            List<Vector3> localWaypoints = new List<Vector3>(track.LocalWaypoints());
            List<Vector3> newWaypoints = new List<Vector3>(localWaypoints.Select(v => v + track.transform.position));
            waypoints = new List<Vector3>(waypoints.Concat(newWaypoints));
        }

        const float speed = 1f;

        var leaderPosition = targets[0].transform.position;
        var tailPosition = targets[targets.Count() - 1].transform.position;
        for (int i = 0; i < targets.Count(); i++)
        {
            GameObject target = targets[i];
            var offsetFromLeader = (leaderPosition - target.transform.position).magnitude;
            var offsetFromTail = (tailPosition - target.transform.position).magnitude;

            Vector3 cachedStart = Vector3.zero;
            Vector3 cachedEnd = Vector3.zero;

            var startingDirectionVector = (waypoints[1] - waypoints[0]).normalized;
            var endingDirectionVector = (waypoints[waypoints.Count() - 1] - waypoints[waypoints.Count() - 2]).normalized;
            cachedStart = waypoints[0];
            cachedEnd = waypoints[waypoints.Count() - 1];
            waypoints[0] -= startingDirectionVector * offsetFromLeader;
            waypoints[waypoints.Count() - 1] += endingDirectionVector * offsetFromTail;

            target.transform.position = waypoints[0];
            var result = target.transform.DOPath(waypoints.ToArray(), (1/speed) * tracks.Count(), PathType.CatmullRom, PathMode.Sidescroller2D);

            waypoints[0] = cachedStart;
            waypoints[waypoints.Count() - 1] = cachedEnd;
            
            result.SetLookAt(.01f, true);
            result.SetEase(Ease.InOutQuad);
        }
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
