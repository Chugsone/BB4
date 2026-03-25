using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceAbleObjects : MonoBehaviour
{
    public bool isGrabbed;
    public PlayerInput player;

    void Start()
    {
        isGrabbed = true;
    }

    private void Update()
    {
        if (isGrabbed)
        {
            Vector2 screenPos = player.GetComponent<CursorLink>().cursor.GetComponent<RectTransform>().position;
            Debug.Log("Grabbing" + screenPos);
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            Debug.Log("World Pos" + worldPos);
            worldPos.x = Mathf.Round(worldPos.x);
            worldPos.y = Mathf.Round(worldPos.y);
            transform.position = worldPos;
        }
    }

    public void Place(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("Place");
            isGrabbed = false;

            player.GetComponent<CursorLink>().cursor.gameObject.SetActive(false);
            player.actionEvents[2].RemoveListener(Place);
        }
    }
}
