using UnityEngine;
using UnityEngine.Playables;

public class FinishEvent : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public GameObject playerObject;
    public GameObject animationPlayer;
    public GameObject timerTextObject;

    private bool hasTriggered = false; // Ʈ���� �ߺ� ������

    private void OnTriggerEnter(Collider other)
    {
        // �̹� ����ưų�, �÷��̾ �ƴ� ��� ����
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true; // �� �̻� ������� �ʵ��� �÷��� ����

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
