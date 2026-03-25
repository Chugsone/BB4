using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorLink : MonoBehaviour
{
    public Cursor cursor;
    private Vector2 StickPosition;

    public void MoveMouse(InputAction.CallbackContext ctx)
    {
        if (!cursor.gameObject.activeSelf)
            return;

        if (cursor)
        {
            Debug.Log("Moving Mouse" + ctx.ReadValue<Vector2>());
            cursor.GetComponent<RectTransform>().position = ctx.ReadValue<Vector2>();
        }
    }
    public void MoveGamePad(InputAction.CallbackContext ctx)
    {
        if (!cursor.gameObject.activeSelf)
            return;

        StickPosition = ctx.ReadValue<Vector2>();
    }
    private void Update()
    {
        if (cursor)
        {
            cursor.GetComponent<RectTransform>().position += (Vector3)StickPosition;
        }
    }
    public void Select(InputAction.CallbackContext ctx)
    {
        if (!cursor.gameObject.activeSelf)
            return;

        Debug.Log("Selecting");
        if (ctx.started)
        {
            PointerEventData data = new(EventSystem.current)
            {
                position = cursor.GetComponent<RectTransform>().position,
                pointerId = cursor.player.GetComponent<PlayerInput>().playerIndex
            };

            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(data, results);

            foreach (var result in results)
            {
                if (result.gameObject.TryGetComponent(out EventTrigger trigger))
                {
                    trigger.OnPointerClick(data);
                }
            }
        }
    }
}
