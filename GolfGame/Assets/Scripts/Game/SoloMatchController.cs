using System;
using UnityEngine;
using TMPro;

public class SoloMatchController : MatchController
{

    [SerializeField] TMP_Text currentPlayerCountText = null;
    [SerializeField] TMP_Text currentHoleText = null;
    [SerializeField] TMP_Text currentHoleParText = null;

    [SerializeField] private TMP_Text[] matchScoreTexts = new TMP_Text[0];
    [SerializeField] private TMP_Text totalScores = null;
    private int[] MatchScores = new int[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private readonly int[] par = new int[18] { 3, 5, 3, 5, 4, 4, 3, 6, 3, 3, 4, 2, 5, 4, 3, 5, 2, 4 };    //TODO  Cambiar esto que es la media de golpes para meter la pelota en le agujero
    private int currentHole = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TODO Cambio de jugador
            ChangeHole();
        }
    }

    private void Start()
    {
        StartMatch();
    }

    public override void StartMatch()
    {
        currentHoleParText.text = par[0].ToString();
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
        MatchScores[currentHole] = 0;
        UpdateMatchUI();
    }

    public override void SetHolesPoints()
    {
        MatchScores[currentHole]++;
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
        matchScoreTexts[currentHole].text = MatchScores[currentHole].ToString();
        currentPlayerCountText.text = MatchScores[currentHole].ToString();

        int count = 0;
        for (int i = 0; i < MatchScores.Length; i++)
        {
            count += MatchScores[i];
        }
        totalScores.text = count.ToString();

    }
}