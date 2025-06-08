using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeElapsed;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timeElapsed += Time.deltaTime;
        timerText.text = FormatTime(timeElapsed);
    }

    string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, hundredths);
    }


    public float GetTime() => timeElapsed;

    public void StopTimer() => isRunning = false;

    public void ResetTimer()
    {
        timeElapsed = 0f;
        isRunning = true;
    }
}
