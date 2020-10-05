using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackInfoPanelSection : MonoBehaviour
{
    public TextMeshProUGUI tracks;

    TrackManager trackManager;


    // Start is called before the first frame update
    void Start()
    {
        trackManager = FindObjectOfType<TrackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        tracks.text = "" + string.Format("{0:#,0}/{1:#,0}", trackManager.CurrentSpecialTrackLength, trackManager.MaximumTrackLength);
    }
}
