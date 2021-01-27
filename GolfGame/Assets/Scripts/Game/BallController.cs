using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody BallPhysiscs { get; set; }
    public bool IsDisplayed { get; set; } = true;
    [SerializeField] private MatchController matchController = null;
    [SerializeField] private GameObject controllersPanel = null;
    [SerializeField] private GameObject mapPanel = null;
    [SerializeField] private GameObject HUDPanel = null;
    [SerializeField] private Transform ballFrontal = null;

    private void Start()
    {
        BallPhysiscs = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (BallPhysiscs.velocity == Vector3.zero)
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
        BallPhysiscs.AddForce(ballFrontal.forward * force * 1000);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            if (SV.isSoloMatch)
            {
                matchController.ChangeScene(other.GetComponent<FinishController>().nextScene);
            }
            else
            {
                matchController.nextScene = other.GetComponent<FinishController>().nextScene;
                matchController.SwapPlayer();
            }
            ResetBall();
        }
    }

    public void ShowGameHUD()
    {
        if (!IsDisplayed)
        {
            IsDisplayed = true;
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
        IsDisplayed = false;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        BallPhysiscs.velocity = Vector3.zero;
    }
}
