﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchUpgradeRow : MonoBehaviour
{
    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI descriptionTMP;

    public TextMeshProUGUI buyButtonPriceTMP;

    public Button buyButton;

    public Image image;

    public UpgradeObject upgrade;

    public int timesBought = 0;

    int _currentCost = 0;
    bool canBuy = false;

    UpgradesManager upgradesManager;


    // Start is called before the first frame update
    void Start()
    {
        UpdateLabels();
        PaintButtonAsUnavailable();
        upgradesManager = FindObjectOfType<UpgradesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLabelForPurchase();
    }

    public void AttemptToPurchaseUpgrade() {
        if (!upgrade.repeats && timesBought > 0) { return; }
        if (upgradesManager.moneyManager.currentBalance < CurrentCost()) { return; }
    
        upgradesManager.moneyManager.AddExpense(CurrentCost());
        timesBought += 1;
        upgradesManager.ApplyUpgrade(upgrade);

        if(!upgrade.repeats) {
            Destroy(gameObject); // TODO: Display the user they bought something....
        } else {
            UpdateLabels();
        }
    }

    void UpdateLabels() {
        _currentCost = CurrentCost();

        titleTMP.text = upgrade.title + (timesBought > 0 ? " (" + (timesBought + 1) + ")" : "");
        descriptionTMP.text = upgrade.description;
        image.sprite = upgrade.image;
        buyButtonPriceTMP.text = "" + string.Format("{0:#,0}", CurrentCost());
    }

    int CurrentCost()
    {
        return (int)(Mathf.Pow(upgrade.priceScaling, timesBought) * upgrade.cost);
    }

    void UpdateLabelForPurchase() {
        if ( upgradesManager.moneyManager.currentBalance >= _currentCost ) {
            if (!canBuy) {
                PaintButtonAsAvailable();
            }
            canBuy = true;
        } else {
            if(canBuy) {
                PaintButtonAsUnavailable();
            }
            canBuy = false;
        }
    }

    void PaintButtonAsAvailable() {
        buyButton.GetComponent<Image>().color = new Color(0.1f, 0.8f, 0.1f, 0.8f);
        buyButton.interactable = true;
    }

    void PaintButtonAsUnavailable() {
        buyButton.GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 0.1f);
        buyButton.interactable = false;
    }
}