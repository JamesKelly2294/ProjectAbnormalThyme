using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public GameObject trainPrefab;
    public List<Train> trains;

    [Header("Active Trains")]
    public List<Train> activeTrains;
    public int activeTrainCapacity = 1;
    public int activeTrainLength
    {
        get { return activeTrains.Count; }
    }

    [Header("Idle Trains")]
    public int idleTrainCapacity = 2;
    public int idleTrainLength = 1;
    public int newTrainMultiplier = 1;
    public float newTrainInterval = 10;
    float newTrainTimer = 0;
    bool isIdleTrainWaiting;
    Train idleTrain;

    [Header("Passenger Queue")]
    public int passengerQueueCapacity = 5;
    public int passengerQueueLength = 1;
    public int newPassengerMultiplier = 1;
    public float newPassengerInterval = 5;
    float newPassengerTimer;

    // Start is called before the first frame update
    void Start()
    {
        newTrainTimer = newTrainInterval - 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateActiveTrains();
        UpdateIdleTrainQueue();
        UpdatePassengerQueue();
    }

    void UpdateActiveTrains()
    {

    }

    void UpdateIdleTrainQueue()
    {
        newTrainTimer += Time.deltaTime;
        if (newTrainTimer > newTrainInterval)
        {
            newTrainTimer = 0;
            idleTrainLength += 1 * newTrainMultiplier;
            idleTrainLength = Mathf.Clamp(idleTrainLength, 0, idleTrainCapacity);
        }

        SpawnIdleTrain();
    }

    void SpawnIdleTrain()
    {
        if (idleTrainLength > 0 && idleTrain == null)
        {
            GameObject newIdleTrainGO = Instantiate(trainPrefab);
            idleTrain = newIdleTrainGO.GetComponent<Train>();
            idleTrain.numberOfPeople = Mathf.Min(4 * idleTrain.numberOfCars, passengerQueueLength);
            passengerQueueLength -= idleTrain.numberOfPeople;
            idleTrain.Initialize();
            idleTrain.LoadTrain();
            idleTrain.GetComponent<TrainTrackFollow>().PullUpToStart();
        }
    }

    void UpdatePassengerQueue()
    {
        newPassengerTimer += Time.deltaTime;
        if (newPassengerTimer > newPassengerInterval)
        {
            newPassengerTimer = 0;
            passengerQueueLength += 1 * newPassengerMultiplier;
            passengerQueueLength = Mathf.Clamp(passengerQueueLength, 0, passengerQueueCapacity);
        }
    }

    public bool IsTrackFull()
    {
        return activeTrainLength >= activeTrainCapacity;
    }

    public bool CanTrackAcceptNewTrain()
    {
        return true;
    }

    public bool IsIdleTrainWaiting()
    {
        if (idleTrain) {
            return idleTrain.isWaiting;
        }  else  {
            return false;
        }
    }
    
    public void SendNewTrain()
    {
        if(idleTrain && !IsTrackFull())
        {
            idleTrain.isBrakingFullStop = false;
            idleTrain.targetSpeed = 2.0f;
            idleTrain.brakingPower = 0.0f;

            activeTrains.Add(idleTrain);

            idleTrainLength -= 1;
            idleTrain = null;
        }
    }

    public void DestroyOldestTrain()
    {
        activeTrains.RemoveAt(0);
    }
}
