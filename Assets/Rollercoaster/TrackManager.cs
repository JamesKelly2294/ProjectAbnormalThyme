using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackManager : MonoBehaviour
{
    public float rideExcitementMultiplier = 1;

    public int MaximumTrackLength { get; private set; }
    public int CurrentSpecialTrackLength { get; private set; }

    public List<Track> Tracks
    {
        get; protected set;
    }

    private Dictionary<TrackType, List<Track>> trackMap = new Dictionary<TrackType, List<Track>>();
    private Dictionary<TrackType, bool> unlockedTracksMap = new Dictionary<TrackType, bool>();

    private void Awake()
    {
        Tracks = new List<Track>();

        unlockedTracksMap.Add(TrackType.Straight, true);
        unlockedTracksMap.Add(TrackType.Loop, true);
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

    public Track TrackAt(Vector2Int coordinates)
    {
        if (Tracks.Count <= 0) { return null; }

        coordinates = new Vector2Int(coordinates.x - Mathf.RoundToInt(Tracks.First().transform.position.x), coordinates.y);

        if (coordinates.x < 0 || coordinates.x >= Tracks.Count) { return null; }

        Track t = Tracks[coordinates.x];

        if (coordinates.y >= t.Height) { return null; }

        return t;
    }

    void CalculateTrackLengths()
    {
        CurrentSpecialTrackLength = Tracks.Where((t) => (t.type != TrackType.Start && t.type != TrackType.End && t.type != TrackType.Straight)).Count();
        MaximumTrackLength = Tracks.Where((t) => (t.type != TrackType.Start && t.type != TrackType.End)).Count();
    }

    public bool CanPurchaseTrackOfType(TrackType type)
    {
        return unlockedTracksMap.ContainsKey(type) && unlockedTracksMap[type];
    }
}
