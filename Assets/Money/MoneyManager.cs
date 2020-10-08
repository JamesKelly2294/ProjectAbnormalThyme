using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    static MoneyManager _instance;
    public static MoneyManager Instance
    {
        get
        {
            if (_instance == null) { 
                _instance = FindObjectOfType<MoneyManager>();
            }
            return _instance;
        }
    }
    public long currentBalance = 0;

    long incomeThisSample = 0;
    LinkedList<long> incomeSamples = new LinkedList<long>();
    public float timePerSample = 1f;
    public int maxSamples = 30;
    public float averageIncomePerSecond = 0;
    private Dictionary<UpgradeParameter, int> unlockedUpgrades = new Dictionary<UpgradeParameter, int>();

    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        t = timePerSample;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if ( t > timePerSample ) {
            t -= timePerSample;

            UpdateSamples();
        }
    }

    // last minute hackiness, should be with an "upgrade manager"
    public bool IsUpgradeApplied(UpgradeParameter upgrade)
    {
        return unlockedUpgrades.ContainsKey(upgrade);
    }

    public float MultiplierForMoneyType(MoneyType type)
    {
        UpgradeParameter upgradeType = UpgradeParameter.greenMoneyValue;
        switch(type)
        {
            case MoneyType.Green:
                upgradeType = UpgradeParameter.greenMoneyValue;
                break;
            case MoneyType.Red:
                upgradeType = UpgradeParameter.redMoneyValue;
                break;
            case MoneyType.Blue:
                upgradeType = UpgradeParameter.blueMoneyValue;
                break;
            case MoneyType.Purple:
                upgradeType = UpgradeParameter.purpleMoneyValue;
                break;
            default:
                return 1.0f;
        }

        if(unlockedUpgrades.ContainsKey(upgradeType))
        {
            return Mathf.Pow(2, unlockedUpgrades[upgradeType]);
        }
        else
        {
            return 1.0f;
        }
    }

    public void ApplyUpgrade(UpgradeParameter upgrade)
    {
        if (unlockedUpgrades.ContainsKey(upgrade))
        {
            unlockedUpgrades[upgrade] += 1;
        }
        else
        {
            unlockedUpgrades[upgrade] = 1;
        }
    }

    public void AddIncome(long income) {
        currentBalance += income;
        incomeThisSample += income;
    }

    public void AddExpense(long expense) {
        currentBalance -= expense;
    }

    void UpdateSamples() {
        if (incomeSamples.Count == maxSamples) {
            incomeSamples.RemoveLast();
        }

        incomeSamples.AddFirst(incomeThisSample);
        incomeThisSample = 0;

        long totalIncomeInCurrentSamples = 0;
        foreach (var sample in incomeSamples)
        {
            totalIncomeInCurrentSamples += sample;
        }


        averageIncomePerSecond = ((float)totalIncomeInCurrentSamples / (float)incomeSamples.Count) / timePerSample ;
    }
}