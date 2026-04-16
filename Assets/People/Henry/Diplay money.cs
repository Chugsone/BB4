using UnityEngine;
using TMPro;

public class Diplaymoney : MonoBehaviour
{
    public TMP_Text tmpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tmpText.text = SaveDataController.Instance.current.Currency.ToString();
    }
}
