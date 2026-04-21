using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Diagnostics.CodeAnalysis;

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

    [SerializeField] private int upgradeID; // Unique ID for this upgrade, used for saving/loading

    int level = 0;

    private void Start()
    {
        List<int> levels = SaveDataController.Instance.current.Upgrades.Levels;
        if (levels != null && levels.Count > 0)
        {
            Debug.Log("TEst");
            // SaveDataController.Instance.current.Upgrades.Levels = new List<int>() {0, 0, 0,0 ,0 ,0 ,0 ,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            level = SaveDataController.Instance.current.Upgrades.Levels[upgradeID];
        }

        UpdateUI();
    }

    public void ClickAction()
    {
        int price = CalculatePrice();

        if (gameManager == null)
        {
            Debug.LogWarning("StoreUpgrade: GameManager is not assigned.");
            return;
        }

        if (upgradeButton != null && !upgradeButton.interactable)
            return;

        bool purchaseSuccessful = gameManager.PurchaseAction(price);
        if (purchaseSuccessful)
        {
            level++;
            SaveDataController.Instance.current.Upgrades.Levels[upgradeID] = level;

            UpdateUI();
        }

        //makes the animator play a apurchase animation if the purchase was successful
        if (purchaseSuccessful != null)
        {
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Purchase");
            }
        }
        //plays last frame when next upgrade is clicked and the purchase is successful
        if (purchaseSuccessful != null)
        {
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("Purchase", -1, 1f);
            }
        }
    }

    public void UpdateUI()
    {
        int price = CalculatePrice();

        if (priceText != null)
            priceText.text = price.ToString();

        if (incomeInfoText != null)
            incomeInfoText.text = $"{level} x {moneyPerUpgrade}/s";

        bool canPurchase = false;
        if (gameManager != null)
            canPurchase = SaveDataController.Instance.current.Currency >= price;

        if (upgradeButton != null)
            upgradeButton.interactable = canPurchase;

        bool isPurchased = level > 0;
        if (upgradeImage != null)
            upgradeImage.color = isPurchased ? Color.white : Color.gray;
        if (upgradeNameText != null)
            upgradeNameText.text = isPurchased ? upgradeName : "???";
    }

    int CalculatePrice()
    {
        float multiplier = (upgradePriceMultiplier <= 0f) ? 1f : upgradePriceMultiplier;
        int basePrice = Mathf.Max(0, startPrice);
        int price = Mathf.RoundToInt(basePrice * Mathf.Pow(multiplier, level));
        return Mathf.Max(0, price);
    }

    public float CalculateMoneyPerSecond()
    {
        return moneyPerUpgrade * level;
    }
}
