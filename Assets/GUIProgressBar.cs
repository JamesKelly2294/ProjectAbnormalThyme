using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIProgressBar : MonoBehaviour
{
    public RectTransform progressBarOuter;
    public RectTransform progressBarInner;

    private float _value;
    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            progressBarInner.sizeDelta = new Vector2(_value * progressBarOuter.rect.width, progressBarInner.sizeDelta.y);
        }
    }

    public void SetValue(float val)
    {
        Value = val;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
