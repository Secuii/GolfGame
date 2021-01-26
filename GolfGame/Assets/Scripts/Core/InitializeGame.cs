using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    private void Awake()
    {
        InitializeDatabase();
        InitializeRanking();
    }

    public void InitializeRanking()
    {
        //TODO Setear los datos en el ranking
    }

    public void InitializeDatabase()
    {
        //TODO Inicializar la base de datos
    }

    public void InitializeSavedFile()
    {
        //TODO Chequear por partidas guardadas
    }
}
