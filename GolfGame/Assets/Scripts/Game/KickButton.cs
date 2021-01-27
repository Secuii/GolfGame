using UnityEngine;
using UnityEngine.EventSystems;

public class KickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private KickBarController barController;
    public void OnPointerDown(PointerEventData eventData)
    {
        barController.ChargingBar = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        barController.ChargingBar = false;
    }
}
