using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



public class ShopScore : MonoBehaviour
{
    [SerializeField]
    private List<Image> playerBars;
    public int playerPoints;
    public int winningPoints = 3;
    public Image playerbar;
    public SaveData saveData;
    public ButtonTest buttonTest;
    [Range(0, 3)] public int currentScore = 0;

    private void Start()
    {
    }

    private void Update()
    {
        playerbar.fillAmount = currentScore / (float)winningPoints;
        playerbar.fillAmount = playerPoints / (float)winningPoints;
    }


}