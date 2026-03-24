using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text countText;
    public TMP_Text incomeText;
    [SerializeField] StoreUpgrade[] storeUpgrades;
    [SerializeField] int updatesPerSecond = 5;
    [SerializeField] int managerID;

    [HideInInspector] public float count = 0;
    float nextIdleTime = 1;
    float lastIncomeValue = 0;

    private int idleIncome = 0;

    private void Start()
    {
        // SaveDataController.Instance.current.Upgrades.Income = new List<int>() {0, 0, 0,0 ,0 ,0 ,0 ,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        if (managerID == -1) 
        {
            SetIndexes();
        } 
        
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
        if (managerID == -1)
        {
            SaveDataController.Instance.current.Currency += idleIncome;
            UpdateUI();
            return;
        }

        float sum = 0;
        foreach (var storeUpgrade in storeUpgrades)
        {
            sum += storeUpgrade.CalculateMoneyPerSecond();
            storeUpgrade.UpdateUI();
        }
        lastIncomeValue = sum;
        SaveDataController.Instance.current.Upgrades.Income[managerID] = (int) sum;
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

    void SetIndexes()
    {
        List<int> t_income = SaveDataController.Instance.current.Upgrades.Income;
        for (int i = 0; i < t_income.Count; i++)
        {
            if (t_income[i] != 0) 
            {
                idleIncome += t_income[i];
            }
        }
    }

    void UpdateUI()
    {
        if (countText != null) countText.text = Mathf.RoundToInt(SaveDataController.Instance.current.Currency).ToString();
        if (managerID == -1)
        {
            if (incomeText != null) incomeText.text = idleIncome.ToString();
            return;
        }
        if (incomeText != null) incomeText.text = lastIncomeValue.ToString();
    }


    void OnDestroy()
    {
       
    }

 
}
