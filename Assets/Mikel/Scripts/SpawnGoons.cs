using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;

public class SpawnGoons : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public List<Goons> goons;
    public List<Button> goonButtons;
    public void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        foreach (var goon in goons)
        {
            GameObject button = Instantiate(ButtonPrefab, transform);
            button.GetComponent<Image>().sprite = goon.icon;
            button.GetComponent<Button>().onClick.AddListener(() => SpawnGameObject(goon));
        }
    }

    public void SpawnGameObject(Goons goon)
    {
        GameObject goonInstance = Instantiate(goon.prefab, Vector3.zero, Quaternion.identity);
        goonInstance.GetComponent<DragDrop>().isGrabbed = true;
        goonInstance.GetComponent<AllyAI>().goonStat = goon;

        FindFirstObjectByType<TeamManager>().TeamA.Add(goonInstance);
    }

    public void ShowButtons() 
    {
        
        foreach (var goon in goonButtons)
        {
            goon.gameObject.SetActive(true);
        }
    }



}
