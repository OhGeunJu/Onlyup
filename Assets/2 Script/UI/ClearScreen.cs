using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClearUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    void Start()
    {
        float clearTime = PlayerPrefs.GetFloat("ClearTime", 0f);
        timerText.text = "CLEAR!!!\n" + FormatTime(clearTime);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    private string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);
        return string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, hundredths);
    }
}
