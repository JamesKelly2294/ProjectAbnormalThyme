using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchPopUp : MonoBehaviour
{

    public ResearchTab lab, hr, marketing;

    public GameObject labTabButton, hrTabButton, marketingTabButton;

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
        GameObject currentTabButton = TabButtonForResearchTabType(currentTab);
        GameObject newTabButton = TabButtonForResearchTabType(newTab);

        RectTransform currentTabButtonRect = currentTabButton.GetComponent<RectTransform>();
        currentTabButtonRect.Translate(0, 1, 0);
        currentTabButtonRect.sizeDelta = new Vector2(currentTabButtonRect.sizeDelta.x, currentTabButtonRect.sizeDelta.y - 2);
        RectTransform currentTabButtonTextRect = currentTabButton.transform.GetChild(0).GetComponent<RectTransform>();
        currentTabButtonTextRect.Translate(0, -1, 0);

        RectTransform newTabButtonRect = newTabButton.GetComponent<RectTransform>();
        newTabButtonRect.Translate(0, -1, 0);
        newTabButtonRect.sizeDelta = new Vector2(newTabButtonRect.sizeDelta.x, newTabButtonRect.sizeDelta.y + 2);
        RectTransform newTabButtonTextRect = newTabButton.transform.GetChild(0).GetComponent<RectTransform>();
        newTabButtonTextRect.Translate(0, 1, 0);

        GetComponent<Image>().color = newTabButton.GetComponent<Image>().color;

        TabContentForResearchTabType(currentTab).gameObject.SetActive(false);
        TabContentForResearchTabType(newTab).gameObject.SetActive(true);
        currentTab = newTab;
    }

    public GameObject TabButtonForResearchTabType(ResearchTabType tabType) {
        switch (tabType) {
            case ResearchTabType.lab:
                return labTabButton;
            case ResearchTabType.hr:
                return hrTabButton;
            case ResearchTabType.marketing:
                return marketingTabButton; 
            default:
                return labTabButton;
        }
    }

    public ResearchTab TabContentForResearchTabType(ResearchTabType tabType) {
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

    
    public void SwitchTabToLab() {
        ChangeTab(ResearchTabType.lab);
    }

    public void SwitchTabToHr() {
        ChangeTab(ResearchTabType.hr);
    }

    public void SwitchTabToMarketing() {
        ChangeTab(ResearchTabType.marketing);
    }
}

public enum ResearchTabType {
    lab, hr, marketing
}