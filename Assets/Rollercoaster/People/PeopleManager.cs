using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{

    public LinkedList<TrainCarPerson> peopleInLine = new LinkedList<TrainCarPerson>();
    public int maxLineLength = 10;

    public float wealthMultiplier = 1.0f;
    public float walletSizeMultiplier = 1.0f;

    public float lineAcceptanceRate = 1f; // How often we check for new people to join the line
    public int newPeopleMultiplier = 1; // How many people actually join the line when the timer is up
    public float t = 0;

    public float chanceOfPurpleSpender = 0.1f;

    public float chanceOfBlueSpender = 0.1f;

    public float chanceOfRedSpender = 0.1f;

    public TrainCarPerson personPrefab;

    public GUIProgressBar peopleProgressBar;









    // Start is called before the first frame update
    void Start()
    {
        peopleProgressBar = GameObject.Find("PeopleProgressBar")?.GetComponent<GUIProgressBar>();
        AddPersonToLine();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > lineAcceptanceRate) {
            t -= lineAcceptanceRate;
            
            for(int i = 0; i < newPeopleMultiplier; i++)
            {
                AddPersonToLine();
            }
        }

        peopleProgressBar?.SetValue(t / lineAcceptanceRate);
    }

    void AddPersonToLine() {
        if (peopleInLine.Count >= maxLineLength) { return; }
        SpenderType nextPersonSpenderType = SpenderTypeOfNextPersonInLine();

        TrainCarPerson person = Instantiate(personPrefab, transform);
        person.spenderType = nextPersonSpenderType;
        person.desriedMoneyInWallet = DesiredMoneyInWalletForSpenderType(nextPersonSpenderType);
        person.moneyInBank = MoneyInBankForSpenderType(nextPersonSpenderType);
        person.gameObject.SetActive(false);

        peopleInLine.AddLast(person);
    }

    public TrainCarPerson PeekPersonInLine() {
        if (peopleInLine.Count == 0) { return null; }

        return peopleInLine.Last.Value;
    }

    public TrainCarPerson PopPersonFromLine()
    {
        if (peopleInLine.Count == 0) { return null; }

        TrainCarPerson person = peopleInLine.Last.Value;
        person.gameObject.SetActive(true);
        peopleInLine.RemoveLast();
        return person;
    }

    SpenderType SpenderTypeOfNextPersonInLine() {
        if ( Random.Range(0, (int)(1f / chanceOfPurpleSpender)) == 0 ) {
            return SpenderType.purple;
        } else if ( Random.Range(0, (int)(1f / chanceOfBlueSpender)) == 0 ) {
            return SpenderType.blue;
        } else if ( Random.Range(0, (int)(1f / chanceOfRedSpender)) == 0 ) {
            return SpenderType.red;
        } else {
            return SpenderType.green;
        }
    }

    int DesiredMoneyInWalletForSpenderType(SpenderType type) {

        int money = 0;
        switch (type) {
            case SpenderType.green:
                money = Random.Range(1, 10); break;
            case SpenderType.red:
                money = Random.Range(200, 500); break;
            case SpenderType.blue:
                money = Random.Range(500, 2_000); break;
            case SpenderType.purple:
                money = Random.Range(2_000, 10_000); break;
        }

        return (int)(walletSizeMultiplier * (float)money);
    }

    int MoneyInBankForSpenderType(SpenderType type) {

        int money = 0;
        switch (type) {
            case SpenderType.green:
                money = Random.Range(100, 500); break;
            case SpenderType.red:
                money = Random.Range(500, 5_000); break;
            case SpenderType.blue:
                money = Random.Range(5_000, 50_000); break;
            case SpenderType.purple:
                money = Random.Range(50_000, int.MaxValue); break;
        }

        return (int)(walletSizeMultiplier * (float)money);
    }
}
