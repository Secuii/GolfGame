using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string playerName { get; set; }
    public int palyerLevel { get; set; }
    public float playerExperience { get; set; }

    [SerializeField] private BallController ballController = null;
    [SerializeField] private SoloMatchController matchController = null;

    public void AddExperience()
    {
        playerExperience = matchController.CheckMatchExperience();
    }

    public void KickBall()
    {
        ballController.KickBall();
    }

}