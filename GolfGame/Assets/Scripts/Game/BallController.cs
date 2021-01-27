using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody ballPhysiscs { get; set; }
    public bool isDisplayed { get; set; } = true;
    [SerializeField] private GameObject controllersPanel = null;
    [SerializeField] private GameObject mapPanel = null;
    [SerializeField] private GameObject HUDPanel = null;
    [SerializeField] private Transform ballFrontal;

    private void Start()
    {
        ballPhysiscs = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (ballPhysiscs.velocity == Vector3.zero)
        {
            ShowGameHUD();
        }
        else
        {
            HideGameHUD();
        }
    }

    public void KickBall(float force)
    {


        ballPhysiscs.AddForce(ballFrontal.forward * force * 1000);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<INteractable>() != null)
        {
            //TODO Llamar para cambiar de jugador
        }
    }

    public void ShowGameHUD()
    {
        if (!isDisplayed)
        {
            isDisplayed = true;
            controllersPanel.SetActive(true);
            mapPanel.SetActive(true);
            HUDPanel.SetActive(true);
        }
    }

    public void HideGameHUD()
    {
            controllersPanel.SetActive(false);
            mapPanel.SetActive(false);
            HUDPanel.SetActive(false);
            isDisplayed = false;        
    }
}
