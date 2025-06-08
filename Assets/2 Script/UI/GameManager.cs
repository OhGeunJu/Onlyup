using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameTimer Timer { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Timer = GetComponent<GameTimer>();
        SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �� �ڵ� ����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Timer != null)
                PlayerPrefs.SetFloat("CurrentTime", Timer.CurrentTime);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ÿ�̸� UI�� �翬��
        if (Timer != null)
        {
            GameObject timerObj = GameObject.Find("Timer");
            if (timerObj != null)
            {
                Timer.timerText = timerObj.GetComponent<TMPro.TextMeshProUGUI>();
                timerObj.SetActive(true);
            }

            Timer.ResetTimer(); // �ð��� ������ ä �ٽ� ����
        }
    }

}
