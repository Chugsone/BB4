using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class pointer : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log (col.gameObject.name);
    }
}
