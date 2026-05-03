using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{


    public List<GameObject> TeamA { get; set; }
    public List<GameObject> TeamB { get; set; }
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
            Debug.Log("Team B wins!");
        }
        else if (TeamB.Count <= 0)
        {
            Debug.Log("Team A wins!");
        }
    }
}
