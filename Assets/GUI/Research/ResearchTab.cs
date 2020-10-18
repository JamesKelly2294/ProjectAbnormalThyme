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
        upgrades.Sort((firstUpgrade, secondUpgrade) =>
        {
            var compareVal = firstUpgrade.repeats.CompareTo(secondUpgrade.repeats);
            if (compareVal == 0)
            {
                compareVal = firstUpgrade.sortingPriority.CompareTo(secondUpgrade.sortingPriority);
                if (compareVal == 0)
                {
                    compareVal = firstUpgrade.title.CompareTo(secondUpgrade.title);
                }
            }

            return compareVal;
        });
        foreach (var upgrade in upgrades) {
            ResearchUpgradeRow row = Instantiate(upgradeRowPrefab, contentList.transform);
            row.upgrade = upgrade;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
