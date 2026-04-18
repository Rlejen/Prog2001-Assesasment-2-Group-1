
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;

    void Start()
    {
        DynamicGI.UpdateEnvironment();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1); 
    }

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        Time.timeScale = 1f; 
    }
}

