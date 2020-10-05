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

    // Start is called before the first frame update
    public void Init()
    {
        if(_building == null) { return; }


        image.sprite = _building.previewImage;
        foreach (var component in _building.buyableBlueprintPrefab.GetComponents<MonoBehaviour>())
        {
            if (component is Buyable)
            {
                MonoBehaviour buyableBehavior = Instantiate(component, transform);
                _buyable = (Buyable)buyableBehavior;
            }
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
                buyButtonPriceTMP.text = _buyable.PurchaseCost().ToString();
            }
        }
    }
}
