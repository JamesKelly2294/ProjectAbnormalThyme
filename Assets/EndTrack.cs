using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Track))]
public class EndTrack : MonoBehaviour
{
    public GameObject newTrackPrefab;

    TrackPlacer trackPlacer;
    TrackManager trackManager;

    // Start is called before the first frame update
    void Start()
    {
        trackManager = FindObjectOfType<TrackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PurchaseNewTrack()
    {
        var coords = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y));
        trackManager.PlaceTrack(newTrackPrefab.GetComponent<Track>(), coords);

        trackManager.PlaceTrack(this.GetComponent<Track>(), coords + Vector2Int.right * 1);
    }
}
