using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class pointer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Error")
        {
            Debug.Log("Error: Please Try Again");
        }
        Debug.Log (col.gameObject.name);
    }
}