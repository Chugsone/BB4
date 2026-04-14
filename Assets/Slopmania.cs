using UnityEngine;

public class Slopmania : MonoBehaviour
{
    public int price;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    // Update is called once per frame
    public void BetterSlop()
    {
        SaveDataController.Instance.current.Currency -= price;
    }
}
