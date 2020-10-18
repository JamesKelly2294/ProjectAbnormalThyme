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

    [Range(0, 100)]
    public int sortingPriority;

    public List<UpgradeEffect> effects;

    public List<UpgradeObject> nextUpgrades;
}

public enum UpgradeParameter {

    lineLength,
    lineRefreshSpeed,
    numberOfCarsInEachTrain,
    numberOfTrains,
    rideExcitement,
    redSpenderTypeChance,
    blueSpenderTypeChance,
    purpleSpenderTypeChance,
    numberOfConcurentTrainsAllowedOnTrack,
    trainCreationRate,
    unlockPhotoStation,
    unlockBank,
    loopSlowdown,
    greenMoneyValue,
    redMoneyValue,
    blueMoneyValue,
    purpleMoneyValue,
    unlockTrainOMatic,
    winTheGame
}

public enum UpgradeOperation {
    add, scale, set
}

[Serializable]
public struct UpgradeEffect {
    public UpgradeParameter parameter;

    public UpgradeOperation operation;

    public float amount;
}