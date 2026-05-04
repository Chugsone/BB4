using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TeamManager : MonoBehaviour
{
    public bool playerWin;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    public List<GameObject> TeamA;
    public List<GameObject> TeamB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //search the heirarchy for the team members and add them to the lists
        List<string> myItems = new List<string>() { "Sword", "Shield", "Potion" };
        int itemCount = myItems.Count; // Returns 3
        Debug.Log("Total items: " + itemCount);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (TeamA.Count <= 0)
        {
            playerWin = false;
            LoseScreen.SetActive(true);
           
        }
        else if (TeamB.Count <= 0)
        {
            playerWin = true;
            WinScreen.SetActive(true);
            
        }
    }
    public void Return()
    {
        SceneManager.LoadScene("2D Test");
    }

}

