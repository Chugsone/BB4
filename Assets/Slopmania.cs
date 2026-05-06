using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Slopmania : MonoBehaviour
{
    public float priceMultiplier = 1.5f;
    public int startPrice = 5;
    [SerializeField] private Allies.AllieNames slopmania;
    private Allies _allies;
    public string upgradeName;
    public float upgradePriceMultiplier;
    public float moneyPerUpgrade;
    
    public AudioClip employmentsfx;

    public GameManager gameManager;
    int price = 0;
    [SerializeField] private int upgradeID; // Unique ID for this upgrade, used for saving/loading
    int level = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (SaveDataController.Instance.current.allies == null)
        //{
        //    SaveDataController.Instance.current.allies = new Allies();
        //    Debug.Log("sjubjdsojogjdo");
        //}
        List<int> levels = SaveDataController.Instance.current.Upgrades.Levels;
        if (levels != null && levels.Count > 0)
        {
            Debug.Log("TEst");
            // SaveDataController.Instance.current.Upgrades.Levels = new List<int>() {0, 0, 0,0 ,0 ,0 ,0 ,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            level = SaveDataController.Instance.current.Upgrades.Levels[upgradeID];
        }

        _allies = SaveDataController.Instance.current.allies;
        if (_allies.Stats.ContainsKey(slopmania))
        {
            price = (int)startPrice * _allies.Data[_allies.Stats[slopmania]].Deaths + 1;
            if (_allies.Data[_allies.Stats[slopmania]].CurrentlyHired)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            _allies.Stats.Add(slopmania, _allies.Stats.Keys.ToArray().Length + 1);
            _allies.Data.Add(new AllieData
            {
                Deaths = 0,
                Exp = 0,
                Level = 1,
            });
            //SaveDataController.Instance.current.allies.Data[_allies.Stats[slopmania]] = new AllieData
            //{
            //    Deaths = 0,
            //    Exp = 0,
            //    Level = 1,
            //};
            //SaveDataController.Instance.current.allies.Stats.Add(slopmania, new AllieData { });
            price = startPrice;
        }

    }

    // Update is called once per frame
    public void BetterSlop()
    {
        if (!(SaveDataController.Instance.current.Currency >= price))
        {
            return;
        }
        SaveDataController.Instance.current.Currency -= price;
        price = startPrice * (_allies.Data[_allies.Stats[slopmania]].Deaths) + 1;
        //AllieData data = SaveDataController.Instance.current.allies.Stats[slopmania];
        //data.CurrentlyHired = true;
        //data.Exp += 0.5f;
        //SaveDataController.Instance.current.allies.Stats[slopmania] = data;
        _allies.Data[_allies.Stats[slopmania]].CurrentlyHired = true;
        _allies.Data[_allies.Stats[slopmania]].Exp++;

        gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(employmentsfx, new Vector2(0,0));
    }

    public void Update()
    {
        Debug.Log($"{SaveDataController.Instance.current.allies.Data[_allies.Stats[slopmania]].CurrentlyHired}");
        
        
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
