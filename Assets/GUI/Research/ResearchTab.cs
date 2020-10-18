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
        upgrades.Sort((first, second) =>
        {
            if (first.sortingPriority != second.sortingPriority)
            {
                return first.sortingPriority.CompareTo(second.sortingPriority);
            }
            else
            {
                if (first.repeats == second.repeats)
                {
                    return first.title.CompareTo(second.title);
                }
                else
                {
                    if (first.repeats)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
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
