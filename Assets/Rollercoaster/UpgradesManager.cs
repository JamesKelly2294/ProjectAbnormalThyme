using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{

    public MoneyManager moneyManager;
    public PeopleManager peopleManager;
    public TrainManager trainManager;
    public TrackManager trackManager;

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
                trainManager.activeTrainCapacity = (int) ApplyOperationOnValue(effect.operation, effect.amount, trainManager.activeTrainCapacity);
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
        }
    }

    float ApplyOperationOnValue(UpgradeOperation operation, float amount, float value) {
        if (operation == UpgradeOperation.scale) {
            return value * amount;
        } else {
            return value + amount;
        }
    }
}
