using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Slopmania : MonoBehaviour
{
    public float priceMultiplier = 1.5f;
    public int startPrice = 5;
    [SerializeField] private Allies.AllieNames slopmania;
    private Allies _allies;


    int price = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (SaveDataController.Instance.current.allies == null)
        //{
        //    SaveDataController.Instance.current.allies = new Allies();
        //    Debug.Log("sjubjdsojogjdo");
        //}



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
    }

    public void Update()
    {
        Debug.Log($"{SaveDataController.Instance.current.allies.Data[_allies.Stats[slopmania]].CurrentlyHired}");
        
    }
}
