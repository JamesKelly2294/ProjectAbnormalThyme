using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Plugins.Core.PathCore;

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
}
