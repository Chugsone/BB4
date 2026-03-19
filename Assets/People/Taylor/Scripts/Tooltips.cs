using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltips : MonoBehaviour
{
    [SerializeField] private GameObject tooltipObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipObject.SetActive(true);
    }

    public void PointerExit(PointerEventData eventData)
    {
        tooltipObject.SetActive(false);

    }
}