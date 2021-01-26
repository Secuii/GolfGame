using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel = null;
    [SerializeField] private GameObject lastPanel = null;

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
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
        lastPanel = currentPanel;
        currentPanel = nextPanel;
    }
    
    public void BackMenu(GameObject currentPanel)
    {
        lastPanel.SetActive(true);
        this.currentPanel.SetActive(false);
        this.currentPanel = lastPanel;
        lastPanel = currentPanel;
    }

    public void StartMatchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
