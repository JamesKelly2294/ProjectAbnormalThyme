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
        if (moneyManager.currentBalance > 100_000)
        {
            balance.text = "" + string.Format("{0:#.###E+00}", moneyManager.currentBalance);
        } else
        {
            balance.text = "" + string.Format("{0:#,0}", moneyManager.currentBalance);
        }

        if (moneyManager.averageIncomePerSecond > 10_000)
        {
            averageIncome.text = "" + string.Format("{0:#.###E+00}", moneyManager.averageIncomePerSecond + "/s");
        }
        else
        {
            averageIncome.text = "" + string.Format("{0:#,0.00}", moneyManager.averageIncomePerSecond + "/s");
        }
    }
}
