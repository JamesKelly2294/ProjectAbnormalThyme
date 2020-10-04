using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainInfoPanelSection : MonoBehaviour
{
    public TextMeshProUGUI idleTrains;
    public TextMeshProUGUI activeTrains;

    TrainManager trainManager;


    // Start is called before the first frame update
    void Start()
    {
        trainManager = FindObjectOfType<TrainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        idleTrains.text = "" + string.Format("{0:#,0}/{1:#,0}", trainManager.idleTrainLength, trainManager.idleTrainCapacity);
        activeTrains.text = "" + string.Format("{0:#,0}/{1:#,0}", trainManager.activeTrainLength, trainManager.activeTrainCapacity);
    }
}
