using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Buyable
{
    int PurchaseCost();
    bool IsPurchasable();
}

[CreateAssetMenu()]
public class Building : ScriptableObject
{
    public Sprite previewImage;
    
    // Must conform to Buyable, no easy way to enforce in editor :( 
    public GameObject buyableBlueprintPrefab;
    
    public GameObject buildingPrefab;
}
