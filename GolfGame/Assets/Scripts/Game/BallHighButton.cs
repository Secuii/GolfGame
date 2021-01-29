using UnityEngine;
using UnityEngine.EventSystems;

public class BallHighButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private BallController ballController = null;
    [SerializeField] private int direction = 1;
    [SerializeField] private float HighSpeed = 30;
    private bool ChangeHigh;

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeHigh = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ChangeHigh = false;
    }

    private void Update()
    {
        if (ChangeHigh)
        {
            if(ballController.BallHigh <= 25 && ballController.BallHigh >= 1)
                ballController.BallHigh += HighSpeed * direction * Time.deltaTime;
        }
    }
}
