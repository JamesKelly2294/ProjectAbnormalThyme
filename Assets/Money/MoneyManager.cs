using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    public long currentBalance = 0;

    int incomeThisSample = 0;
    LinkedList<int> incomeSamples = new LinkedList<int>();
    public float timePerSample = 1f;
    public int maxSamples = 30;
    public float averageIncomePerSecond = 0;

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

    public void AddIncome(int income) {
        currentBalance += income;
        incomeThisSample += income;
    }

    public void AddExpense(int expense) {
        currentBalance -= expense;
    }

    void UpdateSamples() {
        if (incomeSamples.Count == maxSamples) {
            incomeSamples.RemoveLast();
        }

        incomeSamples.AddFirst(incomeThisSample);
        incomeThisSample = 0;    

        int totalIncomeInCurrentSamples = 0;
        foreach (var sample in incomeSamples)
        {
            totalIncomeInCurrentSamples += sample;
        }


        averageIncomePerSecond = ((float)totalIncomeInCurrentSamples / (float)incomeSamples.Count) / timePerSample ;
    }
}