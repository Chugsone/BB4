using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SpawnItems : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public List<Goons> goons;

    public void Spawn()
    {
        foreach (var item in FindObjectsByType<Cursor>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            item.gameObject.SetActive(true);
        }

        List<Goons> shuffleObjects = goons.OrderBy(x => Random.Range(-1f, 1f)).ToList();
        int playercount = FindObjectsByType<PlayerInput>(FindObjectsSortMode.None).Length;

        List<Goons> selectedObjects = shuffleObjects.Take(playercount).ToList();

        foreach (Goons obj in selectedObjects)
        {
            var button = Instantiate(ButtonPrefab, transform).GetComponent<EventTrigger>();

            button.GetComponent<Image>().sprite = obj.icon;
            button.triggers.First(item => item.eventID == EventTriggerType.PointerClick).callback.AddListener(data => StartCoroutine(SpawmGameObject(data, obj.prefab, button.gameObject)));
        }

        gameObject.SetActive(true);
    }

    public IEnumerator SpawmGameObject(BaseEventData data, GameObject obj, GameObject button)
    {
        int playerIndex = (data as PointerEventData).pointerId;

        var newObject = Instantiate(obj, Vector3.zero, Quaternion.identity).GetComponent<PlaceAbleObjects>();
        newObject.player = FindObjectsByType<PlayerInput>(FindObjectsInactive.Include, FindObjectsSortMode.None).First(item => item.playerIndex == playerIndex);
        EventSystem.current.SetSelectedGameObject(null);

        Destroy(button);
        yield return new WaitForSeconds(0.1f);

        newObject.player.actionEvents[2].AddListener(newObject.Place);
    }


    private void Update()
    {
        if (GetComponentsInChildren<Button>().Length == 0)
        {
            //   gameObject.SetActive(false);
        }
    }

}
