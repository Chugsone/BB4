using UnityEngine;

public class CursorFollow : MonoBehaviour
{


    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Business"))
        {
            Debug.Log(collision.gameObject.name);
        }
    }
}
