using TMPro;
using UnityEngine;

public class StoreUpgrade : MonoBehaviour
{
    [Header("Components")]
    public TMP_Text priceText;
    public TMP_Text incomeInfoText;

    [Header("Upgrade Info")]
    public int startPrice;
    public float upgradePriceMultiplier;
    public float moneyPerUpgrade;


    [Header("Managers")]
    public GameManager gameManager;

    int level = 0;

    private void Start()
    {
        UpdateUI();
    }

    public void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSuccessful = gameManager.PurchaseAction(price);
        if (purchaseSuccessful)
        {
            level++;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + " x " + moneyPerUpgrade + "/s";
    }

    int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level));
        return price;
    }

    public float CalculateMoneyPerSecond()
    {
        return moneyPerUpgrade * level;
    }
}
