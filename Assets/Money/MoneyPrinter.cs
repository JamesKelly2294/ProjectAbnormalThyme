using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPrinter : MonoBehaviour
{

    public Money greenMoneyPrefab;
    public Money redMoneyPrefab;
    public Money blueMoneyPrefab;
    public Money purpleMoneyPrefab;

    public int moneyToDispense;
    public float dispenseTime = 0.1f;
    float t = 0;

    public float excitement = 0; // A modifier on dispenseTime

    private MoneyManager moneyManager;


    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * excitement;
        if ( t > dispenseTime ) {
            t = 0; // We are deliberatly throwing away history here as we don't want to build up an insane demand for bills, and spend them at the bank....

            DispenseABill();
        } 
    }

    public void DispenseABill() {
        moneyToDispense = DispenseABill(moneyToDispense);
    }

    // Returns the new count of money
    public int DispenseABill(int current) {
        Money moneyPrefab;
        if (current >= purpleMoneyPrefab.StartingValue && Random.Range(0, 10) >= 8 ) {
            moneyPrefab = purpleMoneyPrefab;
        } else if (current >= blueMoneyPrefab.StartingValue && Random.Range(0, 10) >= 5 ) {
            moneyPrefab = blueMoneyPrefab;
        } else if (current >= redMoneyPrefab.StartingValue && Random.Range(0, 10) >= 2 ) {
            moneyPrefab = redMoneyPrefab;
        } else if (current >= greenMoneyPrefab.StartingValue) {
            moneyPrefab = greenMoneyPrefab;
        } else {
            return 0;
        }


        if (moneyManager != null) {
            moneyManager.AddIncome(moneyPrefab.Value);
        }

        AudioManager.main.PlayMoneyEffect();

        Money instance = Instantiate(moneyPrefab);
        instance.t = Random.Range(0, instance.animationTime);
        instance.transform.position = transform.position;
        Destroy(instance.gameObject, 5f);
        return current - moneyPrefab.StartingValue;
    }
}
