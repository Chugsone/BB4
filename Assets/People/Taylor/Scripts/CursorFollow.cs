using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CursorFollow : MonoBehaviour
{
    [SerializeField] private GameObject tooltipObject;
    [SerializeField] private TMPro.TMP_Text title;
    [SerializeField] private TMPro.TMP_Text info;

    private bool isHovering;

    public void LeftClick(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed && !isHovering)
        {
            return;
        }

        SceneManager.LoadScene("Rian");

    }
    

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Business") && collision.gameObject.TryGetComponent<TooltipData>(out TooltipData data))
        {
            Debug.Log("EEEE");
            tooltipObject.SetActive(true);
            title.text = data.title;
            info.text = data.description;
            isHovering = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Business"))
        {
            tooltipObject.SetActive(false);
            isHovering = false;
        }
    }
}
