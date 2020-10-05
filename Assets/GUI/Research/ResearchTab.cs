using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchTab : MonoBehaviour
{

    public GameObject contentList;

    public ResearchUpgradeRow upgradeRowPrefab;

    public List<UpgradeObject> upgrades;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var upgrade in upgrades) {
            ResearchUpgradeRow row = Instantiate(upgradeRowPrefab, contentList.transform);
            row.upgrade = upgrade;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
