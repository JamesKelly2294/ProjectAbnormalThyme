using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradesManager : MonoBehaviour
{

    public MoneyManager moneyManager;
    public PeopleManager peopleManager;
    public TrainManager trainManager;
    public TrackManager trackManager;

    public ResearchUpgradeRow updateRowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyUpgrade(UpgradeObject upgrade) {
        foreach(var effect in upgrade.effects) {
            ApplyUpgradeEffect(effect);
        }
    }

    void ApplyUpgradeEffect(UpgradeEffect effect) {
        switch (effect.parameter) {
            case UpgradeParameter.lineLength:
                peopleManager.maxLineLength = (int)ApplyOperationOnValue(effect.operation, effect.amount, peopleManager.maxLineLength);
                break;
            case UpgradeParameter.lineRefreshSpeed:
                peopleManager.lineAcceptanceRate = ApplyOperationOnValue(effect.operation, effect.amount, peopleManager.lineAcceptanceRate);
                break;
            case UpgradeParameter.numberOfCarsInEachTrain:
                trainManager.numberOfCars = (int) ApplyOperationOnValue(effect.operation, effect.amount, trainManager.numberOfCars);
                break;
            case UpgradeParameter.numberOfTrains:
                trainManager.idleTrainCapacity = (int) ApplyOperationOnValue(effect.operation, effect.amount, trainManager.idleTrainCapacity);
                break;
            case UpgradeParameter.redSpenderTypeChance:
                peopleManager.chanceOfRedSpender = ApplyOperationOnValue(effect.operation, effect.amount, peopleManager.chanceOfRedSpender);
                break;
            case UpgradeParameter.blueSpenderTypeChance:
                peopleManager.chanceOfBlueSpender = ApplyOperationOnValue(effect.operation, effect.amount, peopleManager.chanceOfBlueSpender);
                break;
            case UpgradeParameter.purpleSpenderTypeChance:
                peopleManager.chanceOfPurpleSpender = ApplyOperationOnValue(effect.operation, effect.amount, peopleManager.chanceOfPurpleSpender);
                break;
            case UpgradeParameter.rideExcitement:
                trackManager.rideExcitementMultiplier = ApplyOperationOnValue(effect.operation, effect.amount, trackManager.rideExcitementMultiplier);
                break;
            case UpgradeParameter.numberOfConcurentTrainsAllowedOnTrack:
                trainManager.activeTrainCapacity = (int)ApplyOperationOnValue(effect.operation, effect.amount, trainManager.activeTrainCapacity);
                break;
            case UpgradeParameter.trainCreationRate:
                trainManager.newTrainInterval = ApplyOperationOnValue(effect.operation, effect.amount, trainManager.newTrainInterval);
                break;
            case UpgradeParameter.unlockPhotoStation:
                trackManager.UnlockTrackType(TrackType.Photo);
                break;
            case UpgradeParameter.unlockBank:
                trackManager.UnlockTrackType(TrackType.Bank);
                break;
            case UpgradeParameter.loopSlowdown:
                trackManager.ApplyUpgrade(TrackTypeUpgrade.LoopSlowdown);
                break;
            case UpgradeParameter.unlockTrainOMatic:
                trackManager.ApplyUpgrade(TrackTypeUpgrade.TrainOMatic);
                break;
            case UpgradeParameter.greenMoneyValue:
                moneyManager.ApplyUpgrade(UpgradeParameter.greenMoneyValue);
                break;
            case UpgradeParameter.redMoneyValue:
                moneyManager.ApplyUpgrade(UpgradeParameter.redMoneyValue);
                break;
            case UpgradeParameter.blueMoneyValue:
                moneyManager.ApplyUpgrade(UpgradeParameter.blueMoneyValue);
                break;
            case UpgradeParameter.purpleMoneyValue:
                moneyManager.ApplyUpgrade(UpgradeParameter.purpleMoneyValue);
                break;
            case UpgradeParameter.winTheGame:
                SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
                break;
        }
    }

    float ApplyOperationOnValue(UpgradeOperation operation, float amount, float value) {
        if (operation == UpgradeOperation.scale) {
            return value * amount;
        } else if (operation == UpgradeOperation.set) {
            return amount;
        } else {
            return value + amount;
        }
    }
}
