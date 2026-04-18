using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private bool isMuted = false;

    [SerializeField] private Image muteButtonImage;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private void Awake()
    {
        instance = this;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            AudioListener.volume = 0f;
            muteButtonImage.sprite = soundOffSprite; 
        }
        else
        {
            AudioListener.volume = 1f;
            muteButtonImage.sprite = soundOnSprite; 
        }
    }
}