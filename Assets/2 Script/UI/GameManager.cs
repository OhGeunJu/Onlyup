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
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 시 자동 연결
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
        // 타이머 UI도 재연결
        if (Timer != null)
        {
            GameObject timerObj = GameObject.Find("Timer");
            if (timerObj != null)
            {
                Timer.timerText = timerObj.GetComponent<TMPro.TextMeshProUGUI>();
                timerObj.SetActive(true);
            }

            Timer.ResetTimer(); // 시간은 유지한 채 다시 실행
        }
    }

}
