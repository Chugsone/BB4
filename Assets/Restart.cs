using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public SpawnGoons spawnGoons;



    public void RestartGame()
    {
        // Destroy all with the ally tag
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in allies)
        {
            Destroy(ally);
        }
        
    }

    public void ResetGame()
    {
        
        spawnGoons.goons.Contains(spawnGoons.goons[0]);
        foreach (var goon in spawnGoons.goonButtons)
        {
            GameObject.FindWithTag("GoonButton").GetComponent<Button>().interactable = true;
            goon.gameObject.SetActive(true);
        }
    }

}

