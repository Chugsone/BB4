using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Goons", menuName = "Scriptable Objects/Goons")]
public class Goons : ScriptableObject
{
    public int Wage;
    public int Strength;
    public int Sales;
    public int Health;



    [TextArea]public string Description;
    public Sprite icon;
    public GameObject prefab;

    internal void SetActive(bool v)
    {
        throw new NotImplementedException();
    }
}
