using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class SpawnGoons : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public List<Goons> goons;

    private void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        foreach (var goon in goons)
        {
            GameObject button = Instantiate(ButtonPrefab, transform);
            button.GetComponent<Image>().sprite = goon.icon;
            button.GetComponent<Button>().onClick.AddListener(() => SpawmGameObject(goon));
        }
    }

    public void SpawmGameObject(Goons goon)
    {
        GameObject goonInstance = Instantiate(goon.prefab, Vector3.zero, Quaternion.identity);
        goonInstance.GetComponent<PlaceAbleObjects>().isGrabbed = true;
    }
}
