using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchTab : MonoBehaviour
{
    public ResearchPopUp popUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTabToLab() {
        popUp.ChangeTab(ResearchTabType.lab);
    }

    public void SwitchTabToHr() {
        popUp.ChangeTab(ResearchTabType.hr);
    }

    public void SwitchTabToMarketing() {
        popUp.ChangeTab(ResearchTabType.marketing);
    }

    public void Close() {
        popUp.ToggleOpen();
    }
}
