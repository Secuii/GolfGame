using UnityEngine;
using UnityEngine.UI;

public class KickBarController : MonoBehaviour 
{
    [SerializeField] private MatchController matchController;
    [SerializeField] private BallController ballController;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image barColor;
    private float Timer = 0f;
    public bool ChargingBar { get; set; }
    private float force = 0f;


    private void Update()
    {
        force = Mathf.Clamp(Timer, 0f, 1f);

        if (ChargingBar)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            if(force != 0)
            {
                matchController.SetHolesPoints();
                ResetTimer();
            }
        }
        barColor.fillAmount = Timer;
        barColor.color = new Color( gradient.Evaluate(force).r,
                                    gradient.Evaluate(force).g,
                                    gradient.Evaluate(force).b,
                                    gradient.Evaluate(force).a
                                    );
    }

    public void ResetTimer()
    {
        ballController.KickBall(force);
        Timer = 0;
    }
}