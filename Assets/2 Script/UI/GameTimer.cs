using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float timeElapsed;
    private bool isRunning = true;

    public float CurrentTime => timeElapsed;

    void Awake()
    {
        PlayerPrefs.DeleteKey("CurrentTime"); // 게임 시작 시 초기화
    }
    void Start()
    {
        timeElapsed = PlayerPrefs.GetFloat("CurrentTime", 0f);
    }

    void Update()
    {
        if (!isRunning) return;

        timeElapsed += Time.deltaTime;

        if (timerText != null)
            timerText.text = FormatTime(timeElapsed);
    }

    public void StopTimer() => isRunning = false;

    public void ResetTimer()
    {
        isRunning = true;
    }

    string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);

        return $"{hours:00}:{minutes:00}:{seconds:00}.{hundredths:00}";
    }
}
