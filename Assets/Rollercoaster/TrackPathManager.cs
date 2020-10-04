using System.Collections.Generic;
using UnityEngine;

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

    // tracks whether or not a train has already triggered a specified section of track
    // once a train leaves the track, it removes itself from the track's hash set
    // once a track is deleted, it removes itself from the dictionary
    // removed because I'm a dumbass and jacob had a much better solution
    //Dictionary<MonoBehaviour, HashSet<MonoBehaviour>> _trackCooldowns;

    public List<Track> Tracks
    {
        get; protected set;
    }

    private void Awake()
    {
        _instance = this;
        //_trackCooldowns = new Dictionary<MonoBehaviour, HashSet<MonoBehaviour>>();
        Tracks = new List<Track>();
    }

    //public void ResetTrainTrackEffects(MonoBehaviour train)
    //{
    //    foreach (var set in _trackCooldowns.Values)
    //    {
    //        set.Remove(train);
    //    }
    //    Debug.Log(_trackCooldowns);
    //}

    //public void TrainTriggeredTrackEffect(MonoBehaviour train, MonoBehaviour track)
    //{
    //    if (!_trackCooldowns.ContainsKey(track))
    //    {
    //        _trackCooldowns[track] = new HashSet<MonoBehaviour>();
    //    }

    //    _trackCooldowns[track].Add(train);
    //    Debug.Log(_trackCooldowns);
    //}

    //public bool HasTrainTriggeredTrackEffectAlready(MonoBehaviour train, MonoBehaviour track)
    //{
    //    if (_trackCooldowns.ContainsKey(track))
    //    {
    //        if (_trackCooldowns[track].Contains(train))
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

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
            //_trackCooldowns.Remove(track);
        }
    }
}
