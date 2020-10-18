using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public delegate void TrainDelegate(Train t);

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

    public int maxPeoplePerCar = 4;

    public int numberOfCars;

    public int numberOfPeople;

    public bool onTrack;
    public bool IsBrakingFullStop { get; set; }
    public bool isWaiting;
    public bool initOnAwake;

    PeopleManager peopleManager;
    TrainManager trainManager;

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
        peopleManager = PeopleManager.Instance;
        trainManager = TrainManager.Instance;

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
            if (IsBrakingFullStop)
            {
                Speed = Mathf.Max(Mathf.Min(Speed, trueTargetSpeed), 0);
                if (Speed == 0)
                {
                    _activeDel?.Invoke(this);
                    _activeDel = null;
                }
            } else
            {
                Speed = Mathf.Max(Mathf.Min(Speed, trueTargetSpeed), minimumSpeed);
            }
        } else
        {
            Speed = Mathf.Max(Mathf.Min(Speed, trueTargetSpeed), -trueTargetSpeed);
        }

        if (markedForDeath && Speed <= 0)
        {
            Destroy(gameObject);
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

    TrainDelegate _activeDel;
    public void BrakeFullStopWithCallback(TrainDelegate del)
    {
        brakingPower = 10;
        IsBrakingFullStop = true;
        _activeDel = del;
    }

    private bool _ghostified;
    public void Ghostify()
    {
        if(_ghostified) { return; }
        _ghostified = true;
        var srs = transform.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < srs.Length; i++)
        {
            var sr = srs[i];
            var color = sr.color;
            sr.color = new Color(color.r, color.g, color.b, color.a * 0.25f);
        }
        MarkForDeath();
    }

    private bool markedForDeath;
    public void MarkForDeath()
    {
        markedForDeath = true;
    }

    private void OnDestroy()
    {
        trainManager.TrainDestroyed(this);
    }
}
