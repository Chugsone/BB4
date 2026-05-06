using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    private Collider2D _col;

    [SerializeField] private UnityEvent onClick;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 2. Convert mouse position to World Space
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            // 3. Fire a 2D ray at the point (Vector2.zero means it only checks that exact point)
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            // 4. Check if the ray hit a collider
            if (hit.collider == _col)
            {
                Debug.Log("Clicked on: " + hit.collider.gameObject.name);
                onClick.Invoke();
            }
        }
    }
}
