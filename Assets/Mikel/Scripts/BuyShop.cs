using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyShop : MonoBehaviour
{
    [SerializeField] private int shopAmount = 1;
    [SerializeField] private Text shopAmountText;

    private void Buy()
    {
        shopAmount += 1;
        shopAmountText.GetComponent<Text>().text = shopAmount.ToString();
    }
}
