using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public GameObject trainPrefab;
    public List<Train> trains;

    public int numberOfCars = 1;

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

    public int passengerQueueCapacity
    {
        get { return peopleManager.maxLineLength; }
    }
    public int passengerQueueLength
    {
        get { return peopleManager.peopleInLine.Count; }
    }

    PeopleManager peopleManager;

    GUIProgressBar idleTrainProgressBar;
    
    void Awake()
    {
        newTrainTimer = newTrainInterval - 1;
        peopleManager = GameObject.Find("PeopleManager")?.GetComponent<PeopleManager>();
        idleTrainProgressBar = GameObject.Find("IdleTrainProgressBar")?.GetComponent<GUIProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateActiveTrains();
        UpdateIdleTrainQueue();

        idleTrainProgressBar?.SetValue(newTrainTimer / newTrainInterval);
    }

    void UpdateActiveTrains()
    {

    }

    void UpdateIdleTrainQueue()
    {
        if (newTrainTimer > newTrainInterval)
        {
            if (idleTrainLength < idleTrainCapacity)
            {

                newTrainTimer = 0;
                idleTrainLength += 1 * newTrainMultiplier;
                idleTrainLength = Mathf.Clamp(idleTrainLength, 0, idleTrainCapacity);
            }
        } else
        {
            newTrainTimer += Time.deltaTime;
        }

        SpawnIdleTrain();
    }

    void SpawnIdleTrain()
    {
        if (idleTrainLength > 0 && idleTrain == null && passengerQueueLength > 0)
        {
            GameObject newIdleTrainGO = Instantiate(trainPrefab);
            idleTrain = newIdleTrainGO.GetComponent<Train>();
            idleTrain.numberOfCars = numberOfCars;
            idleTrain.numberOfPeople = Mathf.Min(idleTrain.maxPeoplePerCar * idleTrain.numberOfCars, passengerQueueLength);
            idleTrain.Initialize();
            idleTrain.LoadTrain();
            idleTrain.GetComponent<TrainTrackFollow>().PullUpToStart();
            idleTrainLength -= 1;
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
    
    public bool SendNewTrain()
    {
        if (idleTrain && !IsTrackFull() && idleTrain.isWaiting)
        {
            AudioManager.main.PlayOneShot(AudioManager.main.trainLaunch, 0.5f);
            idleTrain.IsBrakingFullStop = false;
            idleTrain.targetSpeed = 2.0f;
            idleTrain.brakingPower = 0.0f;

            activeTrains.Add(idleTrain);

            idleTrain = null;
            return true;
        }
        return false;
    }

    public void GhostifyActiveAndIdleTrains()
    {
        idleTrain?.Ghostify();
        foreach (Train t in activeTrains)
        {
            t.Ghostify();
        }
    }

    public void TrainDestroyed(Train t)
    {
        if (idleTrain == t)
        {
            idleTrain = null;
        }
        else
        {
            activeTrains.Remove(t);
        }
    }
}
