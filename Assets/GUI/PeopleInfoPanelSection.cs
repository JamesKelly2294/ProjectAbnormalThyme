using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PeopleInfoPanelSection : MonoBehaviour
{
    public TextMeshProUGUI people;

    PeopleManager peopleManager;


    // Start is called before the first frame update
    void Start()
    {
        peopleManager = FindObjectOfType<PeopleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        people.text = "" + string.Format("{0:#,0}/{1:#,0}", peopleManager.peopleInLine.Count, peopleManager.maxLineLength);
    }
}
