using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    static TrackManager _instance;

    public static TrackManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<TrackManager>();
            }
            return _instance;
        }
    }
    
    public List<Track> Tracks
    {
        get; protected set;
    }

    private void Awake()
    {
        _instance = this;
        Tracks = new List<Track>();
    }

    public void RegisterTrack(Track track)
    {
        if (!Tracks.Contains(track))
        {
            Tracks.Add(track);
            Tracks.Sort((a, b) => (a.transform.position.x.CompareTo(b.transform.position.x)));
        }
    }

    public void UnregisterTrack(Track track)
    {
        if (Tracks.Contains(track))
        {
            Tracks.Remove(track);
        }
    }
}
