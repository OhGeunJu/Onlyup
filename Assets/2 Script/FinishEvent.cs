using UnityEngine;
using UnityEngine.Playables;

public class FinishEvent : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public GameObject playerObject;
    public GameObject animationPlayer;
    public GameObject timerTextObject;

    private bool hasTriggered = false; // 트리거 중복 방지용

    private void OnTriggerEnter(Collider other)
    {
        // 이미 실행됐거나, 플레이어가 아닌 경우 무시
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true; // 더 이상 실행되지 않도록 플래그 설정

        if (timelineDirector != null && timelineDirector.state != PlayState.Playing)
        {
            timelineDirector.Play();

            if (playerObject != null)
                playerObject.SetActive(false);

            if (animationPlayer != null)
                animationPlayer.SetActive(true);

            if (GameManager.Instance != null && GameManager.Instance.Timer != null)
            {
                GameManager.Instance.Timer.StopTimer();
                float finalTime = GameManager.Instance.Timer.CurrentTime;
                PlayerPrefs.SetFloat("ClearTime", finalTime);
            }

            if (timerTextObject != null)
                timerTextObject.SetActive(false);
        }
    }
}
