using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;

public class RouletteWheel : MonoBehaviour
{
    public float Speed = 100f;
    public bool IsSpinning = true;

    void Update()
    {
        Rotate();
    }
    public void Rotate()
    {
      transform.Rotate(0f, 0f, Speed * Time.deltaTime);
        if(IsSpinning == false && Speed > 0f)
        {
            Stop();
        }
    }
    public void Stop()
    {
        Speed--;
        if(Speed <= 0f)
        {
            Speed = 0f;
        }
    }

}
