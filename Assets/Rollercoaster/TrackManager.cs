using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public int MaximumTrackLength { get; private set; }
    public int CurrentSpecialTrackLength { get; private set; }

    public List<Track> Tracks
    {
        get; protected set;
    }

    private Dictionary<TrackType, List<Track>> trackMap = new Dictionary<TrackType, List<Track>>();

    private void Awake()
    {
        _instance = this;
        Tracks = new List<Track>();
    }

    public void RegisterTrack(Track track)
    {
        if (!Tracks.Contains(track))
        {
            for (int i = 0; i < track.Width; i++) {
                Tracks.Add(track);
            }
            Tracks.Sort((a, b) => (a.transform.position.x.CompareTo(b.transform.position.x)));
            
            if(!trackMap.ContainsKey(track.type))
            {
                trackMap[track.type] = new List<Track>();
            }

            trackMap[track.type].Add(track);
            CalculateTrackLengths();
        }
    }

    public void UnregisterTrack(Track track)
    {
        if (Tracks.Contains(track))
        {
            Tracks.RemoveAll((t) => t == track);
            trackMap[track.type].Remove(track);
            CalculateTrackLengths();
        }
    }

    public int CountForTrackType(TrackType type)
    {
        if (trackMap.ContainsKey(type))
        {
            return trackMap[type].Count;
        } else
        {
            return 0;
        }
    }

    void CalculateTrackLengths()
    {
        CurrentSpecialTrackLength = Tracks.Where((t) => (t.type != TrackType.Start && t.type != TrackType.End && t.type != TrackType.Straight)).Count();
        MaximumTrackLength = Tracks.Where((t) => (t.type != TrackType.Start && t.type != TrackType.End)).Count();
    }
}
