using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarPerson : MonoBehaviour
{

    public MoneyPrinter moneyPrinter;

    public int desriedMoneyInWallet;

    public int moneyInBank;

    public SpenderType spenderType = SpenderType.green;

    public SpriteRenderer body, face, hair;



    public List<Sprite> greenBodySprites;
    public List<Sprite> redBodySprites;
    public List<Sprite> blueBodySprites;
    public List<Sprite> purpleBodySprites;
    public List<Sprite> hairSprites;
    public List<Sprite> faceSprites;

    // Start is called before the first frame update
    void Start()
    {
        WithdrawFromBank();

        List<Sprite> bodySprites = greenBodySprites;
        switch (spenderType) {
            case SpenderType.green:
                bodySprites = greenBodySprites; break;
            case SpenderType.red:
                bodySprites = redBodySprites; break;
            case SpenderType.blue:
                bodySprites = blueBodySprites; break;
            case SpenderType.purple:
                bodySprites = purpleBodySprites; break;
        }

        face.sprite = faceSprites[Random.Range(0, faceSprites.Count)];
        hair.sprite = hairSprites[Random.Range(0, hairSprites.Count)];
        body.sprite = bodySprites[Random.Range(0, bodySprites.Count)];
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

public enum SpenderType {
    green = 0,
    red,
    blue,
    purple
}