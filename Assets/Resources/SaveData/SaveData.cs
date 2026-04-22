using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

[Serializable]
public struct SaveData
{
    public UpgradeList Upgrades; //To access this use SaveDataController.Instance.current.UpgradeList.Upgrades
    public int Currency; //To access this use SaveDataController.Instance.current.Currency
    public Allies allies;
}

[Serializable]
public class UpgradeList
{
    public List<int> Levels; //To access this use SaveDataController.Instance.current.UpgradeList.Upgrades
    public List<int> Income; //To access this use SaveDataController.Instance.current.UpgradeList.Income 
    public List<int> playerPoints; //To access this use SaveDataController.Instance.current.UpgradeList.playerPoints 
    public List<int> ShopUpgrades; //To access this use SaveDataController.Instance.current.UpgradeList.ShopUpgrades
}



[Serializable]
public class Allies
{
    public enum AllieNames
    {
        Rian = 0,
        Slop = 1,
        Slope = 2,
        Slopman = 3,
        Slopmania = 4,
    };

}

[Serializable]

public struct AllieData
{
    private int deaths;
    public int Deaths 
    {
        get { return deaths; }
        set 
        {
            deaths = value;
            CurrentlyHired = false;
        }
    }
    public float Exp
    {
        get { return exp; }
        set 
        {
            if (exp >= Level * 100f)
            {
                Level++;
                exp -= Level * 100f;
            }
        }
    }

    private float exp;

    public float Level;

    public bool CurrentlyHired;


}
