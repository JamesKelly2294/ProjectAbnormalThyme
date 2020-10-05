using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksTab : MonoBehaviour
{
    public GameObject contentList;

    public BuildingSlot buildingSlotPrefab;

    public List<Building> buildings;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = contentList.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentList.transform.GetChild(i).gameObject);
        }
        
        foreach (var building in buildings)
        {
            BuildingSlot slot = Instantiate(buildingSlotPrefab, contentList.transform);
            slot.SetBuilding(building);
            slot.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
