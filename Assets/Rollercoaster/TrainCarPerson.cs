using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarPerson : MonoBehaviour
{

    public MoneyPrinter moneyPrinter;

    public int desriedMoneyInWallet;

    public int moneyInBank;


    public int minPossibleDesriedMoneyInWallet, maxPossibleDesriedMoneyInWallet;

    public int minPossibleMoneyInBank, maxPossibleMoneyInBank;

    // Start is called before the first frame update
    void Start()
    {

        desriedMoneyInWallet = Random.Range(minPossibleDesriedMoneyInWallet, maxPossibleDesriedMoneyInWallet);
        moneyInBank = Random.Range(minPossibleMoneyInBank, maxPossibleMoneyInBank);


        WithdrawFromBank();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WithdrawFromBank() {

        int desiredWithdrawAmount = Mathf.Max(desriedMoneyInWallet - moneyPrinter.moneyToDispense, 0);
        int money = Mathf.Min(moneyInBank, desiredWithdrawAmount);
        moneyInBank -= money;
        moneyPrinter.moneyToDispense += money;
    }
}
