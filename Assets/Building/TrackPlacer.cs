using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlacer : MonoBehaviour
{
    public GameObject blueprintGridPrefab;
    public GameObject selectedBlueprintHighlightPrefab;
    public int width = 9;
    public int height = 3;

    public Color validPlacementColor;
    public Color invalidPlacementColor;

    private int minX;
    private int maxX;
    private Camera trackedCamera;

    private GameObject blueprintsGrid;

    public GameObject selectedBlueprint;
    public Building selectedBuilding;
    
    private TrackManager trackManager;
    private MoneyManager moneyManager;

    private TracksTab tracksTab;

    // Start is called before the first frame update
    void Start()
    {
        trackManager = FindObjectOfType<TrackManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        tracksTab = FindObjectOfType<TracksTab>();

        trackedCamera = Camera.main;
        minX = Mathf.FloorToInt(trackedCamera.transform.position.x) - Mathf.FloorToInt(width / 2);
        maxX = Mathf.FloorToInt(trackedCamera.transform.position.x) + Mathf.CeilToInt(width / 2);

        blueprintsGrid = new GameObject("Blueprints Grid");
        blueprintsGrid.transform.position = new Vector3(minX + ((maxX - minX) / 2), 0, 0);

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                GameObject blueprintGrid = Instantiate(blueprintGridPrefab);
                blueprintGrid.transform.position = new Vector3(minX + col, row, 0);
                blueprintGrid.transform.parent = blueprintsGrid.transform;
                blueprintGrid.transform.name = "Blueprint Grid (" + row + ", " + col + ")";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PollInput();
        UpdateBlueprintsGrid();
        UpdateSelectedBlueprintVisuals();
    }

    void UpdateBlueprintsGrid()
    {
        blueprintsGrid.SetActive(selectedBlueprint != null);

        if (blueprintsGrid.activeInHierarchy)
        {
            minX = Mathf.FloorToInt(trackedCamera.transform.position.x) - Mathf.FloorToInt(width / 2);
            maxX = Mathf.FloorToInt(trackedCamera.transform.position.x) + Mathf.CeilToInt(width / 2);

            blueprintsGrid.transform.position = new Vector3(minX + ((maxX - minX) / 2), 0, 0);
        }
    }

    void UpdateSelectedBlueprintVisuals()
    {
        if (!selectedBlueprint) { return; }
        selectedBlueprint.transform.position = trackedCamera.ScreenToWorldPoint(Input.mousePosition);
        selectedBlueprint.transform.position = new Vector3(
            Mathf.Round(selectedBlueprint.transform.position.x), 
            Mathf.Max(0, Mathf.Round(selectedBlueprint.transform.position.y)), 
            0);

        SpriteRenderer highlight = selectedBlueprint.transform.Find("Highlight").gameObject.GetComponent<SpriteRenderer>();
        highlight.color = IsPlacementValid() ? validPlacementColor : invalidPlacementColor;

    }

    bool IsPlacementValid()
    {
        if (selectedBlueprint == null) { return false; }

        Vector2Int coords = GetSelectedBlueprintCoords();

        if (coords.y != 0) { return false; }

        Track newTrack = selectedBlueprint.GetComponent<Track>();
        long newTrackCost = newTrack.PurchaseCost();
        
        Track oldTrack = trackManager.TrackAt(GetSelectedBlueprintCoords());
        if (!oldTrack) { return false; } // can only replace existing tracks
        if (oldTrack.type == newTrack.type) { return false; }
        if (oldTrack.type == TrackType.Start || oldTrack.type == TrackType.End) { return false; }
        long oldTrackRefund = oldTrack.RefundAmount();

        long totalCost = -oldTrackRefund + newTrackCost;

        if (moneyManager.currentBalance < totalCost) { return false; }

        return true;
    }

    Vector2Int GetSelectedBlueprintCoords()
    {
        return new Vector2Int(
            Mathf.RoundToInt(selectedBlueprint.transform.position.x),
            Mathf.Max(0, Mathf.RoundToInt(selectedBlueprint.transform.position.y)));
    }

    void PollInput()
    {
        if (IsPlacementValid() && Input.GetMouseButtonDown(0))
        {
            var placementPosV3 = trackedCamera.ScreenToWorldPoint(Input.mousePosition);
            var placementPos = new Vector2Int(Mathf.RoundToInt(placementPosV3.x), Mathf.RoundToInt(placementPosV3.y));
            var coords = GetSelectedBlueprintCoords();
            if (placementPos.x != coords.x || placementPos.y != coords.y) { return; }
            if (PurchaseAndPlaceBuilding(selectedBuilding, coords))
            {
                AudioManager.main.PlayButtonClick();
            } else
            {
                AudioManager.main.PlayButtonClickFailure();
            }
        } else if (selectedBlueprint != null && Input.GetMouseButtonDown(0))
        {
            AudioManager.main.PlayButtonClickFailure();
        }
    }

    public void SelectBuilding(Building b)
    {
        if (selectedBuilding == b)
        {
            b = null;
        }

        if (b != null)
        {
            Buyable buyable = b.GetBuyable();
            if (buyable is Object)
            {
                buyable = (Buyable)Instantiate((Object)buyable, transform);
                if (!buyable.IsPurchasable()) { b = null; }
                Destroy((Object)buyable);
            }
        }

        Destroy(selectedBlueprint);
        selectedBuilding = null;
        selectedBlueprint = null;

        if (b == null)
        {
            return;
        }

        GameObject newBlueprint = b.buyableBlueprintPrefab;
        selectedBuilding = b;
        selectedBlueprint = Instantiate(selectedBuilding.buyableBlueprintPrefab);
        GameObject highlight = Instantiate(selectedBlueprintHighlightPrefab);
        highlight.transform.parent = selectedBlueprint.transform;
        highlight.transform.name = "Highlight";
        var srs = highlight.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < srs.Length; i++)
        {
            srs[i].sortingOrder -= 10;
        }
        UpdateSelectedBlueprintVisuals();
    }

    public bool PurchaseAndPlaceBuilding(Building b, Vector2Int coords)
    {
        if(b == null)
        {
            return false;
        }
        bool success = false;
        Track newTrack = b.buildingPrefab.GetComponent<Track>();
        Track oldTrack = trackManager.TrackAt(coords);
        long totalCost = 0;

        totalCost += newTrack.PurchaseCostForNthTrack(trackManager.CountForTrackType(newTrack.type) + 1);
        totalCost -= oldTrack.RefundAmount();
        
        if(totalCost > moneyManager.currentBalance)
        {
            return false;
        }

        if (newTrack && oldTrack)
        {
            newTrack = trackManager.PlaceTrack(newTrack, coords);
            success = newTrack != null;
            if (success) { 
                success = true;
            }
        }

        if(success)
        {
            moneyManager.currentBalance -= totalCost;
            return true;
        }
        else
        {
            return false;
        }

    }
}
