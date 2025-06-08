using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject clearPanel;
    public GameObject fadeScreen;
    public GameObject animationPlayer;

    void Awake()
    {
        // 싱글톤 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 필요하면 유지
    }

    void Start()
    {
        HideClearPanel();
        ShowFadeScreen();
        HideAnimationPlayer();
    }

    public void ShowClearPanel()
    {
        if (clearPanel != null)
            clearPanel.SetActive(true);
    }

    public void HideClearPanel()
    {
        if (clearPanel != null)
            clearPanel.SetActive(false);
    }

    public void ShowFadeScreen()
    {
        if (fadeScreen != null)
            fadeScreen.SetActive(true);
    }

    public void HideAnimationPlayer()
    {
        if (animationPlayer != null)
            animationPlayer.SetActive(false);
    }
}
