using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltips : MonoBehaviour
{
    [SerializeField] private GameObject tooltipObject;

    public void OnPointerEnter()
    {
        tooltipObject.SetActive(true);
    }

    public void PointerExit()
    {
        tooltipObject.SetActive(false);

    }
}