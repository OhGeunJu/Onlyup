using UnityEngine;
using UnityEngine.Playables;

public class FinishEvent : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public GameObject playerObject;        // 플레이어 오브젝트
    public GameObject animationPlayer;      // 오브젝트 애니메이션
    public GameTimer gameTimer;           // 타이머 스크립트
    public GameObject timerTextObject;    // 타이머 UI 텍스트 (ex: TextMeshProUGUI가 붙은 오브젝트)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timelineDirector != null && timelineDirector.state != PlayState.Playing)
            {
                timelineDirector.Play();

                // 1. 플레이어 비활성화
                if (playerObject != null)
                    playerObject.SetActive(false);

                // 2. 애니메이션 플레이어 활성화
                if (animationPlayer != null)
                {
                    animationPlayer.SetActive(true);
                }

                // 3. 타이머 멈추고 저장
                if (gameTimer != null)
                {
                    gameTimer.StopTimer();
                    float finalTime = gameTimer.GetTime();
                    PlayerPrefs.SetFloat("ClearTime", finalTime);
                }

                // 4. 타이머 UI 숨기기
                if (timerTextObject != null)
                    timerTextObject.SetActive(false);
            }
        }
    }
}
