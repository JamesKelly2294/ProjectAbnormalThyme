using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchPopUp : MonoBehaviour
{

    public ResearchTab lab, hr, marketing;

    public ResearchTabType currentTab = ResearchTabType.lab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOpen() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ChangeTab(ResearchTabType newTab) {
        TabForResearchTabType(currentTab).gameObject.SetActive(false);
        TabForResearchTabType(newTab).gameObject.SetActive(true);
        currentTab = newTab;
    }

    public ResearchTab TabForResearchTabType(ResearchTabType tabType) {
        switch (tabType) {
            case ResearchTabType.lab:
                return lab;
            case ResearchTabType.hr:
                return hr;
            case ResearchTabType.marketing:
                return marketing; 
            default:
                return lab;
        }
    }
}

public enum ResearchTabType {
    lab, hr, marketing
}