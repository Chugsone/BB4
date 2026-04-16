using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Restart : MonoBehaviour
{
    public SpawnGoons spawnGoons;



    public void RestartGame()
    {
        //delete all existing goons
        List<GameObject> goons = new List<GameObject>(GameObject.FindGameObjectsWithTag("Goons"));
        foreach (var goon in goons)
        {
            Destroy(goon);
        }
        
        //un hide the goon clones
        List<GameObject> goonClones = new List<GameObject>(GameObject.FindGameObjectsWithTag("GoonsClone"));
        foreach (var goonClone in goonClones)
        {
            goonClone.SetActive(true);
        }
    }
}
