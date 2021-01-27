using UnityEngine;
using UnityEngine.EventSystems;

public class RotateBallButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject ball = null;
    [SerializeField] private int direction = 1;
    [SerializeField] private float rotateSpeed = 30;
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
            ball.transform.Rotate(new Vector3(0, rotateSpeed, 0) * direction * Time.deltaTime, Space.World);
        }
    }
}
