using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class Slots : MonoBehaviour
{
    char[] numbersRand = { '0', '1', '2', '3', '4', '5', '6', '7', };
    public TMP_Text slotNum;
    public float time;

    private void Update()
    {
        slotNum.text = numbersRand[Random.Range(0, numbersRand.Length)].ToString();
    }
    public void OffEnable() 
    {
        Invoke("DelayNum", time);
    }
    public void OnEnable()
    {
        enabled = true;

    }
    void DelayNum()
    {
        enabled = false;
    }

}
