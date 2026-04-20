using UnityEngine;

public class Slopmania : MonoBehaviour
{
    public float priceMultiplier = 1.5f;
    public int startPrice = 5;
    [SerializeField] private Allies.AllieNames slopmania;
    int price = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SaveDataController.Instance.current.allies.Stats.ContainsKey(slopmania))
        {
            price = (int)startPrice * SaveDataController.Instance.current.allies.Stats[slopmania].Deaths + 1;
        }
        else
        {
            SaveDataController.Instance.current.allies.Stats[slopmania] = new AllieData 
            {
                    Deaths = 0,
                    Exp = 0,
                    Level = 1,
            };
        }
    }

    // Update is called once per frame
    public void BetterSlop()
    {
        SaveDataController.Instance.current.Currency -= price;
        price = startPrice * SaveDataController.Instance.current.allies.Stats[slopmania].Deaths + 1;
        AllieData data = SaveDataController.Instance.current.allies.Stats[slopmania];
        data.CurrentlyHired = true;
        SaveDataController.Instance.current.allies.Stats[slopmania] = data;
        gameObject.SetActive(false);
    }

    public void Update()
    {
        
        
    }
}
