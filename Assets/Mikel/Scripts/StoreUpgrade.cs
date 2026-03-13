using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class StoreUpgrade : MonoBehaviour
{
    [Header("Components")]
    public TMP_Text priceText;
    public TMP_Text incomeInfoText;
    public Button upgradeButton;
    public Image upgradeImage;
    public TMP_Text upgradeNameText;

    [Header("Upgrade Info")]
    public string upgradeName;
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

    public void UpdateUI()
    {
        priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + " x " + moneyPerUpgrade + "/s";
        bool canPurchase = gameManager.count >= CalculatePrice();
        upgradeButton.interactable = canPurchase;

        bool ispurchased = level > 0;
        upgradeImage.color = ispurchased ? Color.white : Color.gray;
        upgradeNameText.text = ispurchased ? upgradeName : "???";
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
