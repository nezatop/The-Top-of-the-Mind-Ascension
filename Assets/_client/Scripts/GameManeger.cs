using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        MenuController.OnPlayed += Play;
        MenuController.OnPaused += Pause;
        MenuController.OnRestart += Pause;
        MenuController.OnFight += Pause;
        MenuController.OnDialog += Pause;
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
    }
}
