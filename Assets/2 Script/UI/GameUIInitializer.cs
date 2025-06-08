using UnityEngine;

public class GameUIInitializer : MonoBehaviour
{
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject fadeScreen;
    [SerializeField] private GameObject animationPlayer;

    void Start()
    {
        HideClearPanel();
        ShowFadeScreen();
        HideAnimationPlayer();
    }

    public void ShowClearPanel()
    {
        if (clearPanel != null)
            clearPanel.SetActive(true);
    }

    public void HideClearPanel()
    {
        if (clearPanel != null)
            clearPanel.SetActive(false);
    }

    public void ShowFadeScreen()
    {
        if (fadeScreen != null)
            fadeScreen.SetActive(true);
    }

    public void HideAnimationPlayer()
    {
        if (animationPlayer != null)
            animationPlayer.SetActive(false);
    }
}
