using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadReadowanScene()
    {
        SceneManager.LoadScene("ReadowanSceneStart");
    }

    public void LoadNavodScene()
    {
        GameObject persistentLight = GameObject.Find("Directional Light");

        if (persistentLight != null)
        {
            Destroy(persistentLight);
        }

        SceneManager.LoadScene("Navodya_Scene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}