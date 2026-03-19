using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text countText;
    public TMP_Text incomeText;
    [SerializeField] StoreUpgrade[] storeUpgrades;
    [SerializeField] int updatesPerSecond = 5;

    [HideInInspector] public float count = 0;
    float nextIdleTime = 1;
    float lastIncomeValue = 0;


    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (nextIdleTime < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextIdleTime = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
        }
    }

    void IdleCalculate()
    {
        float sum = 0;
        foreach (var storeUpgrade in storeUpgrades)
        {
            sum += storeUpgrade.CalculateMoneyPerSecond();
            storeUpgrade.UpdateUI();
        }
        lastIncomeValue = sum;
        SaveDataController.Instance.current.Currency += (int)(sum / updatesPerSecond);
        UpdateUI();
    }

    public void ClickAction()
    {
        SaveDataController.Instance.current.Currency++;
        SaveDataController.Instance.current.Currency += (int)(lastIncomeValue * 0.02f);
        UpdateUI();
    }

    public bool PurchaseAction(int cost)
    {
        if (SaveDataController.Instance.current.Currency >= cost)
        {
            SaveDataController.Instance.current.Currency -= cost;
            UpdateUI();
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        if (countText != null) countText.text = Mathf.RoundToInt(SaveDataController.Instance.current.Currency).ToString();
        if (incomeText != null) incomeText.text = lastIncomeValue.ToString();
    }


    void OnDestroy()
    {
       
    }

 
}
