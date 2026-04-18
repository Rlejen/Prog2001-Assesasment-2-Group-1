using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject menu;

    void Start()
    {
        DynamicGI.UpdateEnvironment();
    }

    public void LoadReadowanScene()
    {
        SceneManager.LoadScene("ReadowanScene");
    }

    public void LoadNMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowHelpPanel()
    {
        helpPanel.SetActive(true);
        menu.SetActive(false);
    }

    public void HideHelpPanel()
    {
        helpPanel.SetActive(false);
        menu.SetActive(true);
    }
}
