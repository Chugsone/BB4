using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Goons", menuName = "Scriptable Objects/Goons")]
public class Goons : ScriptableObject
{
    public float Wage;
    public float Strength;
    public float Sales;
    public float Health;
    public RuntimeAnimatorController animator;

    [TextArea]public string Description;
    public Sprite icon;
    public GameObject prefab;

    internal void SetActive(bool v)
    {
        throw new NotImplementedException();
    }
}
