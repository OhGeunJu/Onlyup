using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowTimeScale = 0.3f;
    public float normalTimeScale = 1f;

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = slowTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // 물리 연산 보정
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Time.timeScale = normalTimeScale;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
