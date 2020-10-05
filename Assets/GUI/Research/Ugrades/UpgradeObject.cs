using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute()]
public class UpgradeObject : ScriptableObject
{
    public string title;
    public string description;
    public int cost;
    public Sprite image;

    public bool repeats;

    public float priceScaling;

    public List<UpgradeEffect> effects;
}

public enum UpgradeParameter {

    // Labs -- On Going
    lineLength,
    lineRefreshSpeed,
    numberOfCarsInEachTrain,
    numberOfTrains,
    rideExcitement,

    // HR - On Going

    // Marketing -- On Going
    redSpenderTypeChance,
    blueSpenderTypeChance,
    purpleSpenderTypeChance
}

public enum UpgradeOperation {
    add, scale
}

[Serializable]
public struct UpgradeEffect {
    public UpgradeParameter parameter;

    public UpgradeOperation operation;

    public float amount;
}