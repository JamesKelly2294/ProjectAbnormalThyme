using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlacer : MonoBehaviour
{
    public GameObject blueprintPrefab;
    public int width = 9;
    public int height = 3;

    private int minX;
    private int maxX;
    private Camera trackedCamera;

    private GameObject blueprintsGrid;

    public List<GameObject> trackBlueprints;
    private List<GameObject> _trueTrackBlueprints;

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
                GameObject blueprintGrid = Instantiate(blueprintPrefab);
                blueprintGrid.transform.position = new Vector3(minX + col, row, 0);
                blueprintGrid.transform.parent = blueprintsGrid.transform;
                blueprintGrid.transform.name = "Blueprint Grid (" + row + ", " + col + ")";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (blueprintsGrid.activeInHierarchy)
        {
            minX = Mathf.FloorToInt(trackedCamera.transform.position.x) - Mathf.FloorToInt(width / 2);
            maxX = Mathf.FloorToInt(trackedCamera.transform.position.x) + Mathf.CeilToInt(width / 2);

            blueprintsGrid.transform.position = new Vector3(minX + ((maxX - minX) / 2), 0, 0);

        }
    }
}
