using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;

public class RouletteWheel : MonoBehaviour
{
    public float Speed = 100f;
    public bool IsSpinning = true;

    public GameObject pointer;
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
            pointer.GetComponent<BoxCollider2D>().enabled = true;
            Speed = 0f;
        }
    }

}
