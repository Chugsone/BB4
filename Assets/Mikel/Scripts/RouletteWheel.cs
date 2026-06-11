using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RouletteWheel : MonoBehaviour
{
    public float Speed = 100f;
    public bool IsSpinning = true;
    public float maxSpeed = 600f;

    public GameObject pointer;

    private void Start()
    {
        Speed = maxSpeed;
    }
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
        IsSpinning = false;
        Speed--;
        if(Speed <= 0f)
        {
            pointer.GetComponent<BoxCollider2D>().enabled = true;
            Speed = 0f;
        }
    }

    public void Reset()
    {
        IsSpinning = true;
        pointer.GetComponent<BoxCollider2D>().enabled = false;
        Speed = maxSpeed;
    }



}
