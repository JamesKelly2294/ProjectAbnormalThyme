using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{

    public List<TrainCar> cars;

    public TrainCar carPrefab;
    public TrainCar frontCarPrefab;

    public float carWidth;

    public float frontCarWidth;

    public float Speed
    {
        get; protected set;
    }

    [Range(0, 10)]
    public float brakingPower = 0; // negative multiplier on acceleration

    [Range(0, 5)]
    public float acceleration = 1; // units/second^2

    [Range(0, 10)]
    public float targetSpeed = 0;

    [Range (0, 10)]
    public float maxSpeed = 5;
    
    [Range(0, 10)]
    public float minimumSpeed = 1;

    public int numberOfCars;

    public int numberOfPeople;

    public bool onTrack;
    public bool isBrakingFullStop;
    public bool isWaiting;
    public bool initOnAwake;

    PeopleManager peopleManager;

    // Start is called before the first frame update
    void Start()
    {
        if(initOnAwake)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        Vector3 offset = new Vector3(0, 0, 0);
        peopleManager = FindObjectOfType<PeopleManager>();
        
        for (int i = 0; i < numberOfCars; i++)
        {

            TrainCar prefab = carPrefab;
            float xoffset = carWidth;

            if (i == 0)
            {
                prefab = frontCarPrefab;
                xoffset = frontCarWidth;
            }

            TrainCar car = Instantiate(prefab, transform);
            car.train = this;
            car.transform.localPosition = offset;
            offset -= new Vector3(xoffset, 0, 0);
           
            cars.Add(car);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        maxSpeed = Mathf.Max(minimumSpeed, maxSpeed);

        Speed += (acceleration * (brakingPower > 0 ? -brakingPower : 1)) * Time.fixedDeltaTime;
        float trueTargetSpeed = Mathf.Max(Mathf.Min(targetSpeed, maxSpeed), -maxSpeed);
        if (brakingPower > 0)
        {
            if (isBrakingFullStop)
            {
                Speed = Mathf.Max(Mathf.Min(Speed, trueTargetSpeed), 0);
            } else
            {
                Speed = Mathf.Max(Mathf.Min(Speed, trueTargetSpeed), minimumSpeed);
            }
        } else
        {
            Speed = Mathf.Max(Mathf.Min(Speed, trueTargetSpeed), -trueTargetSpeed);
        }
    }

    public void LoadTrain() {
        foreach(var car in cars)
        {
            while ( numberOfPeople > 0 ) {
                TrainCarPerson person = peopleManager.PeekPersonInLine();
                if (person != null) {
                    if (!car.Add(person)) { break; }
                    peopleManager.PopPersonFromLine();
                    numberOfPeople -= 1;
                } else {
                    break;
                }
            }
        }
    }
}
