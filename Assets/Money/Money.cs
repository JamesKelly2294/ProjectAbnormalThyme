using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoneyType
{
    Green,
    Red,
    Blue,
    Purple
}

public class Money : MonoBehaviour
{

    public static MoneyManager c_moneyManager;

    public MoneyType type;

    public AnimationCurve xAcceleration;
    public AnimationCurve yAcceleration;

    public float xAccelerationScale;
    public float yAccelerationScale;

    public float t = 0;
    public float animationTime = 2;

    public float rotationRate = 100;

    public int StartingValue = 1;
    public int Value
    {
        get {
            return Mathf.RoundToInt(StartingValue * MoneyManager.Instance.MultiplierForMoneyType(type));
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        t = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        if (t > animationTime)
        {
            t -= animationTime;
        }

        Vector3 offset = new Vector3(xAcceleration.Evaluate(t / animationTime) * xAccelerationScale, yAcceleration.Evaluate(t / animationTime) * yAccelerationScale, 0f);
        transform.localPosition += offset;

        transform.Rotate(new Vector3(0, 0, rotationRate * Time.fixedDeltaTime));
    }
}
