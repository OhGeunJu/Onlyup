using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public UI_FadeScreen fadeScreen;
    public GameObject setFadeScreen;
    public float fadeDuration = 3f;

    private void Start()
    {
        ShowFadeScreen();
    }

    public void ShowFadeScreen()
    {
        if (setFadeScreen != null)
            setFadeScreen.SetActive(true);
    }

    public void OnStartGame()
    {
        StartCoroutine(StartGameWithFade());
    }

    private IEnumerator StartGameWithFade()
    {
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeDuration);
        }

        SceneManager.LoadScene("GameScene");
    }

    public void OnOpenSettings()
    {
        Debug.Log("설정 화면 열기 (미구현)");
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
