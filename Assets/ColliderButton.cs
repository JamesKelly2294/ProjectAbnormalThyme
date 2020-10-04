using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderButton : MonoBehaviour
{
    public UnityEvent buttonPressed = new UnityEvent();

    private void OnMouseDown()
    {
        if (buttonPressed != null)
        {
            buttonPressed.Invoke();
        }
    }
}
