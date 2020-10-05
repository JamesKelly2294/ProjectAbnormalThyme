using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
    public TextMeshProUGUI buyButtonPriceTMP;

    public Button buyButton;

    public Image image;
    
    private Building _building;
    public void SetBuilding(Building b)
    {
        _building = b;
    }

    public TrackType trackType;

    Buyable _buyable;
    TrackPlacer _trackPlacer;

    // Start is called before the first frame update
    public void Init()
    {
        _trackPlacer = FindObjectOfType<TrackPlacer>();
        if (_building == null) { return; }


        image.sprite = _building.previewImage;


        Buyable buyable = _building.GetBuyable();
        if (buyable is Object)
        {
            _buyable = (Buyable)Instantiate((Object)buyable, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_buyable != null)
        {

            if(!_buyable.IsPurchasable())
            {
                image.color = Color.black;
                buyButtonPriceTMP.text = "???";
            } else
            {
                image.color = Color.white;
                buyButtonPriceTMP.text = "" + string.Format("{0:#,0}", _buyable.PurchaseCost());
            }
        }
    }

    public void BuildingSelected()
    {
        _trackPlacer.SelectBuilding(_building);
    }
}
