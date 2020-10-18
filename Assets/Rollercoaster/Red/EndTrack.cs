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
        trackManager = TrackManager.Instance;
        moneyManager = MoneyManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackManager.TrackCanBeExtended && trackManager.CostToExtendTrack <= moneyManager.currentBalance)
        {
            purchasablePopUp.SetActive(true);
        } else
        {
            purchasablePopUp.SetActive(false);
        }
    }

    public void PurchaseNewTrack()
    {
        if(!trackManager.TrackCanBeExtended) { return; }
        var coords = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y));
        trackPlacer.PurchaseAndPlaceBuilding(newTrackBuilding, coords);
    }
}
