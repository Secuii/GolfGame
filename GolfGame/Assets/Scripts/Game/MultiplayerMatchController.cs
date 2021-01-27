using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiplayerMatchController : MatchController
{
    public Players[] players = new Players[4];
    
    [SerializeField] TMP_Text currentPlayerText = null;
    [SerializeField] TMP_Text currentPlayerCountText = null;
    [SerializeField] TMP_Text currentHoleText = null;
    [SerializeField] TMP_Text currentHoleParText = null;

    [SerializeField] GameObject scoreContainer = null;

    private readonly int[] par = new int[18] { 3, 5, 3, 5, 4, 4, 3, 6, 3, 3, 4, 2, 5, 4, 3, 5, 2, 4 };    //TODO  Cambiar esto que es la media de golpes para meter la pelota en le agujero
    private int currentHole = 0;
    private int currentPlayer = 0;
    private int playerCount = 2;


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TODO Cambio de jugador
            SwapPlayer();
        }
    }

    private void Start()
    {
        playerCount = SV.numberPlayer;
        for (int i = 0; i < playerCount + 1; i++)
        {
            scoreContainer.transform.GetChild(i).gameObject.SetActive(true);
        }
        StartMatch();
    }

    public override void StartMatch()
    {
        currentHoleParText.text = par[0].ToString();

    }

    public override void SwapPlayer()
    {
        currentPlayer++;
        currentPlayerCountText.text = "0";
        currentPlayerText.text = (currentPlayer + 1).ToString();
        if (currentPlayer == playerCount + 1)
        {
            currentPlayer = 0;
            currentPlayerText.text = (currentPlayer + 1).ToString();
            ChangeHole();
        }
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
    }

    public override void SetHolesPoints()
    {
        players[currentPlayer].MatchScores[currentHole]++;
        currentPlayerCountText.text = players[currentPlayer].MatchScores[currentHole].ToString();
        UpdateMatchUI();
    }

    public override void FinishGame()
    {
        Debug.Log("Juego terminado");

    }

    public override void UpdateMatchUI()
    {
        players[currentPlayer].matchScoreTexts[currentHole].text = players[currentPlayer].MatchScores[currentHole].ToString();

        int count = 0;
        for (int i = 0; i < players[currentPlayer].MatchScores.Length; i++)
        {
            count += players[currentPlayer].MatchScores[i];
        }
        players[currentPlayer].totalScores.text = count.ToString();

    }
}

[Serializable]
public class Players
{
    public string name;
    public TMP_Text[] matchScoreTexts = new TMP_Text[0];
    public TMP_Text totalScores = null;
    public int[] MatchScores = new int[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
}