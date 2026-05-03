using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class TeamManager : MonoBehaviour
{

    public List<GameObject> teamMembers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        List<int> enemiesRemaining = new List<int>();
        if (teamMembers.Count == 0)
        {
            Debug.Log("You win!");
        }
    }
}
