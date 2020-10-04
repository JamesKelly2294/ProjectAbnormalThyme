using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlacer : MonoBehaviour
{
    public GameObject blueprintGridPrefab;
    public GameObject selectedBlueprintHighlightPrefab;
    public int width = 9;
    public int height = 3;

    private int minX;
    private int maxX;
    private Camera trackedCamera;

    private GameObject blueprintsGrid;

    public List<GameObject> trackBlueprints;
    public GameObject selectedBlueprint;

    private int selectedBlueprintIndex;

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
