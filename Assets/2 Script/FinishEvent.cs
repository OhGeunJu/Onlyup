using UnityEngine;
using UnityEngine.Playables;

public class FinishEvent : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public GameObject playerObject;        // �÷��̾� ������Ʈ
    public GameObject animationPlayer;      // ������Ʈ �ִϸ��̼�
    public GameTimer gameTimer;           // Ÿ�̸� ��ũ��Ʈ
    public GameObject timerTextObject;    // Ÿ�̸� UI �ؽ�Ʈ (ex: TextMeshProUGUI�� ���� ������Ʈ)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timelineDirector != null && timelineDirector.state != PlayState.Playing)
            {
                timelineDirector.Play();

                // 1. �÷��̾� ��Ȱ��ȭ
                if (playerObject != null)
                    playerObject.SetActive(false);

                // 2. �ִϸ��̼� �÷��̾� Ȱ��ȭ
                if (animationPlayer != null)
                {
                    animationPlayer.SetActive(true);
                }

                // 3. Ÿ�̸� ���߰� ����
                if (gameTimer != null)
                {
                    gameTimer.StopTimer();
                    float finalTime = gameTimer.GetTime();
                    PlayerPrefs.SetFloat("ClearTime", finalTime);
                }

                // 4. Ÿ�̸� UI �����
                if (timerTextObject != null)
                    timerTextObject.SetActive(false);
            }
        }
    }
}
