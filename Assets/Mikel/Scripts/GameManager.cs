using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text countText;
    [SerializeField] TMP_Text incomeText;
    [SerializeField] StoreUpgrade[] storeUpgrades;
    [SerializeField] int updatesPerSecond = 5;


    float count = 0;
    float nextIdleTime = 1;
    float lastIncomeValue = 0;

    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if(nextIdleTime < Time.timeSinceLevelLoad)
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
            sum += storeUpgrade.IdleIncome;
        }
        lastIncomeValue = sum;
        count += sum / updatesPerSecond;
        UpdateUI();
    }

    public void ClickAction()
    {
        count++;
        UpdateUI();
    }

    public bool PurchaseAction(int cost)
    {
        if (count >= cost)
        {
            count -= cost;
            UpdateUI();
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        countText.text = Mathf.RoundToInt(count).ToString();
        incomeText.text = lastIncomeValue.ToString();
    }
}
