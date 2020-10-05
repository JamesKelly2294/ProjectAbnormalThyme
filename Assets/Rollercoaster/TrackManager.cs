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
    private Dictionary<TrackTypeUpgrade, int> unlockedUpgrades = new Dictionary<TrackTypeUpgrade, int>();
    private TrainManager trainManager;

    private void Awake()
    {
        trainManager = FindObjectOfType<TrainManager>();
        Tracks = new List<Track>();

        unlockedTracksMap.Add(TrackType.Straight, true);
        unlockedTracksMap.Add(TrackType.Loop, true);
    }

    public float AutoTrainLauncherTime()
    {
        if(!IsUpgradeApplied(TrackTypeUpgrade.TrainOMatic)) { return 9999999; }
        return 5.0f / unlockedUpgrades[TrackTypeUpgrade.TrainOMatic];
    }

    public bool IsUpgradeApplied(TrackTypeUpgrade upgrade)
    {
        return unlockedUpgrades.ContainsKey(upgrade);
    }

    public void ApplyUpgrade(TrackTypeUpgrade upgrade)
    {
        if(unlockedUpgrades.ContainsKey(upgrade))
        {
            unlockedUpgrades[upgrade] += 1;
        } else
        {
            unlockedUpgrades[upgrade] = 1;
        }
    }

    public void UnlockTrackType(TrackType type)
    {
        unlockedTracksMap.Add(type, true);
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
            UpdateCostToExtendTrack();
        }
    }

    public void UnregisterTrack(Track track)
    {
        if (Tracks.Contains(track))
        {
            Tracks.RemoveAll((t) => t == track);
            trackMap[track.type].Remove(track);
            CalculateTrackLengths();
            UpdateCostToExtendTrack();
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

    private int _costToExtendTrack;
    public int CostToExtendTrack { get; private set; }

    public void UpdateCostToExtendTrack()
    {
        const int baseCost = 1000;
        const float rateOfGrowth = 2.0f;
        int numOwned = MaximumTrackLength - 5; // start with 5, so subtract from total. TODO(james) - calculate starting tracks dynamically?

        CostToExtendTrack = Mathf.RoundToInt(baseCost * (Mathf.Pow(rateOfGrowth, numOwned)));
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

    public Track PlaceTrack(Track trackPrefab, Vector2Int coordinates)
    {
        Track oldtrack = TrackAt(coordinates);

        if (oldtrack)
        {
            if(oldtrack.type == TrackType.End)
            {
                oldtrack.transform.position = new Vector3(coordinates.x + 1, coordinates.y, 0);
            } else
            {
                Destroy(oldtrack.gameObject);
            }
        }

        Track newTrack = Instantiate(trackPrefab, transform);
        newTrack.transform.position = new Vector3(coordinates.x, coordinates.y, 0);

        trainManager.GhostifyActiveAndIdleTrains();
        
        return newTrack;
    }
}
