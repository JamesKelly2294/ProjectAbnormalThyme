using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Track))]
public class EndTrack : MonoBehaviour
{
    public Building newTrackBuilding;
    public GameObject purchasablePopUp;

    TrackPlacer trackPlacer;
    TrackManager trackManager;
    MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        trackPlacer = FindObjectOfType<TrackPlacer>();
        trackManager = FindObjectOfType<TrackManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackManager.CostToExtendTrack <= moneyManager.currentBalance)
        {
            purchasablePopUp.SetActive(true);
        } else
        {
            purchasablePopUp.SetActive(false);
        }
    }

    public void PurchaseNewTrack()
    {
        var coords = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y));
        trackPlacer.PurchaseAndPlaceBuilding(newTrackBuilding, coords);
    }
}
