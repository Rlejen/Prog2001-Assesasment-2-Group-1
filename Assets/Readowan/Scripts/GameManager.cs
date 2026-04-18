using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("All draggable items in the scene")]
    public DragItem[] allItems;

    public void ResetAllItems()
    {
        foreach (DragItem item in allItems)
        {
            if (item != null)
            {
                item.ReturnToTray();
            }
        }
    }

    public void LoadNMenu()
    {

        SceneManager.LoadScene("ReadowanSceneStart");
    }



}