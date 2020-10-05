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

    public List<GameObject> trackBlueprints;
    public GameObject selectedBlueprint;

    private int selectedBlueprintIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
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
        if (!selectedBlueprint) { return false; }

        Vector2Int coords = GetSelectedBlueprintCoords();

        if (coords.y != 0) { return false; }

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
        int newSelectedBlueprintIndex = -2;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            newSelectedBlueprintIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            newSelectedBlueprintIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            newSelectedBlueprintIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            newSelectedBlueprintIndex = 3;
        }

        if (newSelectedBlueprintIndex != -2)
        {
            if (newSelectedBlueprintIndex == selectedBlueprintIndex)
            {
                newSelectedBlueprintIndex = -1;
            }
            GameObject newSelectedBlueprint = null;

            Destroy(selectedBlueprint);
            if (newSelectedBlueprintIndex >= 0 && newSelectedBlueprintIndex < trackBlueprints.Count)
            {
                newSelectedBlueprint = trackBlueprints[newSelectedBlueprintIndex];
                selectedBlueprint = Instantiate(newSelectedBlueprint);
                GameObject highlight = Instantiate(selectedBlueprintHighlightPrefab);
                highlight.transform.parent = selectedBlueprint.transform;
                highlight.transform.name = "Highlight";
                var srs = highlight.GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < srs.Length; i++)
                {
                    srs[i].sortingOrder -= 10;
                }
            } 

            selectedBlueprintIndex = newSelectedBlueprintIndex;
        }
    }
}
