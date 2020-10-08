using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Buyable
{
    long PurchaseCost();
    bool IsPurchasable();
}

[CreateAssetMenu()]
public class Building : ScriptableObject
{
    public Sprite previewImage;

    // Must conform to Buyable, no easy way to enforce in editor :( 
    public GameObject buyableBlueprintPrefab;

    public GameObject buildingPrefab;

    private Buyable _buyable;
    public Buyable GetBuyable()
    {
        if (_buyable != null) { return _buyable; }
        foreach (var component in buyableBlueprintPrefab.GetComponents<MonoBehaviour>())
        {
            if (component is Buyable)
            {
                _buyable = (Buyable)component;
            }
        }
        return _buyable;
    }

    private void Awake()
    {
        _buyable = null;
    }
}
