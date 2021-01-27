using UnityEngine;

public abstract class MatchController : MonoBehaviour
{
    [SerializeField] private GameObject mapPanel = null;
    private bool isScoreShow = false;
    public string nextScene { get; set; }


    public virtual void StartMatch() { }
    public virtual void SwapPlayer() { }
    public virtual void ChangeHole() { }
    public virtual void SetHolesPoints() { }
    public virtual void FinishGame() { }
    public virtual void UpdateMatchUI() { }
    public virtual void ChangeScene(string name) { }
    public readonly int[] par = new int[18] { 3, 5, 3, 5, 4, 4, 3, 6, 3, 3, 4, 2, 5, 4, 3, 5, 2, 4 };    //TODO  Cambiar esto que es la media de golpes para meter la pelota en le agujero


    public void ScoreButton(Animator score)
    {
        if (!isScoreShow)
        {
            score.Play("ShowScorePanel");
            isScoreShow = true;
        }
        else
        {
            score.Play("HideScorePanel");
            isScoreShow = false;
        }
    }

    public void DisplayMap()
    {
        mapPanel.SetActive(!mapPanel.activeSelf);
    }
}