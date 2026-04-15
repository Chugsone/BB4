using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

[Serializable]
public struct SaveData
{
    public UpgradeList Upgrades; //To access this use SaveDataController.Instance.current.UpgradeList.Upgrades
    public int Currency; //To access this use SaveDataController.Instance.current.Currency
}

[Serializable]
public class UpgradeList
{
    public List<int> Levels; //To access this use SaveDataController.Instance.current.UpgradeList.Upgrades
    public List<int> Income; //To access this use SaveDataController.Instance.current.UpgradeList.Income 
    public List<int> playerPoints; //To access this use SaveDataController.Instance.current.UpgradeList.playerPoints
}