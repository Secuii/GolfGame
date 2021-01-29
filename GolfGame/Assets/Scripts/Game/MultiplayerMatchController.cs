using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MultiplayerMatchController : MatchController
{
    public Players[] players = new Players[4];
    
    [SerializeField] TMP_Text currentPlayerText = null;
    [SerializeField] TMP_Text currentPlayerCountText = null;
    [SerializeField] TMP_Text currentHoleText = null;
    [SerializeField] TMP_Text currentHoleParText = null;

    [SerializeField] private TMP_Text[] TopWinners = new TMP_Text[4];
    [SerializeField] private TMP_Text[] playerTop= new TMP_Text[4];
    
    ScoreTuple<int, string> totalScore = new ScoreTuple<int, string>();

    [SerializeField] GameObject scoreContainer = null;
    [SerializeField] GameObject stadisticsPanel = null;
    [SerializeField] GameObject scorePlayers = null;

    private int currentHole = 0;
    private int currentPlayer = 0;
    private int playerCount = 2;

    private void Start()
    {

        SV.isSoloMatch = false;
        playerCount = SV.numberPlayer;
        for (int i = 0; i < playerCount + 1; i++)
        {
            scoreContainer.transform.GetChild(i).gameObject.SetActive(true);
            scorePlayers.transform.GetChild(i).gameObject.SetActive(true);
        }
        StartMatch();
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
        ChangeHole();
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
            ChangeScene(nextScene);
        }
    }

    public override void ChangeHole()
    {
        currentHole++;
        currentHoleText.text = (currentHole + 1).ToString();
        currentHoleParText.text = par[currentHole].ToString();
        if (currentHole + 1 == 2)
        {
            currentHoleText.text = currentHole.ToString();
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
        CheckWinner();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CheckWinner();
        }
    }

    private void CheckWinner()
    {

        for (int i = 0; i < playerCount + 1; i++)
        {
            totalScore.Add(int.Parse(players[i].totalScores.text),"P" + (i+1) );
        }
        totalScore.Sort();

        for (int i = 0; i < totalScore.Count; i++)
        {
            playerTop[i].text = totalScore[i].Item2;
            TopWinners[i].text = totalScore[i].Item1.ToString();
        }

        stadisticsPanel.SetActive(true);
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
public class Players : IComparable
{
    public string name;
    public TMP_Text[] matchScoreTexts = new TMP_Text[0];
    public TMP_Text totalScores = null;
    public int[] MatchScores = new int[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


    public int CompareTo(object obj)
    {
        return 1;
    }
}

public class ScoreTuple<T1, T2> : List<Tuple<T1, T2>>
{
    public void Add(T1 item, T2 item2)
    {
        Add(new Tuple<T1, T2>(item, item2));
    }
}