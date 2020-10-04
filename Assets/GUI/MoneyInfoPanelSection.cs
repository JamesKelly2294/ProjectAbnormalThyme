using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyInfoPanelSection : MonoBehaviour
{

    public TextMeshProUGUI balance;
    public TextMeshProUGUI averageIncome;

    MoneyManager moneyManager;


    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        balance.text = "" + string.Format("{0:#,0}", moneyManager.currentBalance);
        averageIncome.text = "" + string.Format("{0:#,0.00}", moneyManager.averageIncomePerSecond) + "/s";
    }
}
