using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel = null;

    public void StartMultiplayerMatch()
    {
        SceneManager.LoadScene("MultiplayerMatch");
    }

    public void StartSoloMatch()
    {
        SceneManager.LoadScene("MultiplayerMatch");
    }

    public void ChangeMenuPanel(GameObject nextPanel)
    {
        MenuPanel.SetActive(false);
        nextPanel.SetActive(true);
    }
    
    public void BackMenu(GameObject currentPanel)
    {
        MenuPanel.SetActive(true);
        currentPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
