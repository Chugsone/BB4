using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct SaveData
{
    public SkinList UnlockedSkins;
    public int Currency; //To access this use SaveDataController.Instance.current.Currency
}

[Serializable]
public class SkinList
{
    public List<string> Skins; //To access this use SaveDataController.Instance.current.UnlockedSkins.Skins    
}