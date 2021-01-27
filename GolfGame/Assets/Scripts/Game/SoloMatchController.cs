using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SoloMatchController : MatchController
{
    [SerializeField] TMP_Text currentPlayerCountText = null;
    [SerializeField] TMP_Text currentHoleText = null;
    [SerializeField] TMP_Text currentHoleParText = null;

    [SerializeField] private TMP_Text[] matchScoreTexts = new TMP_Text[0];
    [SerializeField] private TMP_Text totalScores = null;
    private int[] matchScores = new int[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private int currentHole = 0;

    private void Start()
    {
        StartMatch();
        SV.isSoloMatch = true;
    }

    public override void StartMatch()
    {
        currentHoleParText.text = par[0].ToString();
        SceneManager.LoadSceneAsync("Hole1", LoadSceneMode.Additive);
        SV.oldScene = "Hole1";

    }

    public override void ChangeScene(string nextScene)
    {
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SV.oldScene);
        SV.oldScene = nextScene;
    }

    public override void ChangeHole()
    {
        currentHole++;
        currentHoleText.text = (currentHole + 1).ToString();
        currentHoleParText.text = par[currentHole].ToString();
        if (currentHole + 1 == 19)
        {
            currentHoleText.text = "18";
            FinishGame();
        }
        matchScores[currentHole] = 0;
        UpdateMatchUI();
    }

    public override void SetHolesPoints()
    {
        matchScores[currentHole]++;
        UpdateMatchUI();
    }

    public override void FinishGame()
    {
        Debug.Log("Juego terminado");

    }

    public float CheckMatchExperience()
    {
        return 0f;
    }

    public override void UpdateMatchUI()
    {
        matchScoreTexts[currentHole].text = matchScores[currentHole].ToString();
        currentPlayerCountText.text = matchScores[currentHole].ToString();

        int count = 0;
        for (int i = 0; i < matchScores.Length; i++)
        {
            count += matchScores[i];
        }
        totalScores.text = count.ToString();

    }
}