using UnityEngine;
using UnityEngine.EventSystems;

public class RotateBallButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject ballLeftRight = null;
    [SerializeField] private BallController ballController = null;
    [SerializeField] private GameObject ballUpDown = null;
    [SerializeField] private int direction = 1;
    [SerializeField] private float rotateSpeedHorizontal = 30;
    [SerializeField] private float rotateSpeedVertical = 30;
    private bool RotateBall;

    public void OnPointerDown(PointerEventData eventData)
    {
        RotateBall = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RotateBall = false;
    }

    private void Update()
    {
        if (RotateBall)
        {
            ballLeftRight.transform.Rotate(new Vector3(0, rotateSpeedHorizontal, 0) * direction * Time.deltaTime, Space.World);
            ballUpDown.transform.Translate(new Vector3(0, 0, rotateSpeedVertical) * direction * Time.deltaTime);
        }
    }
}
