using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DragDrop : MonoBehaviour
{
    public bool isGrabbed;

    private void OnMouseDown()
    {
        isGrabbed = true;
    }

    private void Update()
    {
        if (isGrabbed)
        {
            transform.position = GetMousePositionInWorldSpace();
        }
    }

    private void OnMouseUp()
    {
        isGrabbed = false;
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}
