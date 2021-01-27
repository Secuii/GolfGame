using UnityEngine;
using UnityEngine.EventSystems;

public class MapButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject mapPanel = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        mapPanel.SetActive(!mapPanel.activeSelf);
    }
}
