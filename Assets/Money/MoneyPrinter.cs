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




    // Start is called before the first frame update
    void Start()
    {
        t = dispenseTime; // Makes it so that we dispense as soon as we hit the first spot.
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * excitement;
        if ( t > dispenseTime ) {
            t -= dispenseTime;

            moneyToDispense = DispenseABill(moneyToDispense);
        } 
    }

    // Returns the new count of money
    int DispenseABill(int current) {
        Money moneyPrefab;
        if (current >= purpleMoneyPrefab.value && Random.Range(0, 10) >= 8 ) {
            moneyPrefab = purpleMoneyPrefab;
        } else if (current >= blueMoneyPrefab.value && Random.Range(0, 10) >= 5 ) {
            moneyPrefab = blueMoneyPrefab;
        } else if (current >= redMoneyPrefab.value && Random.Range(0, 10) >= 2 ) {
            moneyPrefab = redMoneyPrefab;
        } else if (current >= greenMoneyPrefab.value ) {
            moneyPrefab = greenMoneyPrefab;
        } else {
            return 0;
        }

        Money instance = Instantiate(moneyPrefab);
        instance.t = Random.Range(0, instance.animationTime);
        instance.transform.position = transform.position;
        Destroy(instance.gameObject, 5f);
        return current - moneyPrefab.value;
    }
}
