using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody BallPhysiscs { get; set; }

    public Transform target;
    //public float BallHigh;
    public float gravity;



    public bool IsDisplayed { get; set; } = true;
    [SerializeField] private MatchController matchController = null;
    [SerializeField] private LineRenderer predictionLine = null;
    [SerializeField] private GameObject controllersPanel = null;
    [SerializeField] private GameObject mapPanel = null;
    [SerializeField] private GameObject HUDPanel = null;
    [SerializeField] private GameObject ArrowDirection = null;
    [SerializeField] private Transform ballFrontal = null;
    public float BallHigh = 25;
    private float BallForce;

    private void Start()
    {
        predictionLine.startWidth = 6;
        predictionLine.endWidth = 6;
        BallPhysiscs = GetComponent<Rigidbody>();
    }

    public void LineEnd()
    {
        predictionLine.SetPosition(0, new Vector3(transform.position.x, 120,transform.position.z));
        predictionLine.SetPosition(1, new Vector3(ArrowDirection.transform.position.x, 120, ArrowDirection.transform.position.z));
        DrawPath();
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
        LineEnd();
        StabilizeHigh();        
    }

    private void StabilizeHigh()
    {
        if(BallHigh >= 25)
        {
            BallHigh = 24.9f;
        }
        else if(BallHigh <= 1)
        {
            BallHigh = 1.1f;
        }
    }

    public void KickBall(float force)
    {
        BallForce = Mathf.Lerp(10,1,force);
        BallHigh = Mathf.Lerp(1, BallHigh, force);
        Launch();

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
            ArrowDirection.SetActive(true);
        }
    }

    public void HideGameHUD()
    {
        controllersPanel.SetActive(false);
        mapPanel.SetActive(false);
        HUDPanel.SetActive(false);
        IsDisplayed = false;
        ArrowDirection.SetActive(false);
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        BallPhysiscs.velocity = Vector3.zero;
    }
    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        BallPhysiscs.useGravity = true;
        BallPhysiscs.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - BallPhysiscs.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - BallPhysiscs.position.x, 0, target.position.z - BallPhysiscs.position.z);
        float time = Mathf.Sqrt(-2 * BallHigh / gravity) + Mathf.Sqrt(2 * (displacementY - BallHigh) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * BallHigh);
        Vector3 velocityXZ = (displacementXZ/BallForce) / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    LaunchData CalculateLaunchDataDraw()
    {
        float displacementY = target.position.y - BallPhysiscs.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - BallPhysiscs.position.x, 0, target.position.z - BallPhysiscs.position.z);
        float time = Mathf.Sqrt(-2 * BallHigh / gravity) + Mathf.Sqrt(2 * (displacementY - BallHigh) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * BallHigh);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchDataDraw();
        Vector3 previousDrawPoint = BallPhysiscs.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = BallPhysiscs.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }

}
