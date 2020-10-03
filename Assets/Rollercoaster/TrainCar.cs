using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar : MonoBehaviour
{
    public Train train;
    public List<TrainCarPerson> people;

    public List<float> peopleXPositions;
    public float peopleYPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Add(TrainCarPerson person)
    {

        if (people.Count >= peopleXPositions.Count) { return false; }

        person.transform.parent = transform;
        person.transform.localPosition = new Vector3(peopleXPositions[people.Count], peopleYPosition, 0);
        people.Add(person);

        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MoneyDrainZone zone = other.gameObject.GetComponent<MoneyDrainZone>();
        if ( zone != null ) {
            foreach (var person in people)
            {
                if ( zone.oneShot ) {
                    person.moneyPrinter.DispenseABill();
                } else {
                    person.moneyPrinter.excitement = zone.excitement;
                }
            }
        }
    } 

    private void OnTriggerExit2D(Collider2D other)
    {
        MoneyDrainZone zone = other.gameObject.GetComponent<MoneyDrainZone>();
        if ( zone != null ) {
            foreach (var person in people)
            {
                person.moneyPrinter.excitement = 0;
            }
        }
    }
}
