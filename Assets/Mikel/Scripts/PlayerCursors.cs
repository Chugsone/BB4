using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCursors : MonoBehaviour
{
    public GameObject cursor;


    public void SpawnCursor(PlayerInput playerInput)
    {
        GameObject cursorInstance = Instantiate(cursor, transform);
        playerInput.GetComponent<CursorLink>().cursor = cursorInstance.GetComponent<Cursor>();
        cursorInstance.GetComponent<Cursor>().player = playerInput.GetComponent<CombatPlayer>();
        cursorInstance.GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, Screen.height / 2);

        cursorInstance.SetActive(false);
    }
}