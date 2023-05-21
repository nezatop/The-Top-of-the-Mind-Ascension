using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public enum MenuName
    {
        Main,
        Play,
        Pause,
        Restart,
        Fight,
        Dialog
    }

    [SerializeField] private GameObject StaticInterface;

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject RestartMenu;
    [SerializeField] private GameObject FightMenu;
    [SerializeField] private GameObject DialogMenu;

    [SerializeField] private GameObject HUD;

    //[SerializeField] private GameObject DialogInterface;
    //[SerializeField] private GameObject FightInterface;

    public MenuName Menu = MenuName.Main;

    public static Action OnMain;
    public static Action OnPlayed;
    public static Action OnPaused;
    public static Action OnRestart;
    public static Action OnFight;
    public static Action OnDialog;

    private void Start()
    {
        Health.OnPlayerDead += Restart;
        Enemy.StartFight += Fight;
        Enemy.EnemyDie += Play;
        NPC.OpenDialogMenu += OpenDialog;
        Dialog.EndDialog += Play;

        switch (Menu)
        {
            case MenuName.Main:
                BackToMainMenu(); break;
            case MenuName.Play:
                Play(); break;
            case MenuName.Pause:
                Pause(); break;
            case MenuName.Restart:
                Restart(); break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        Health.OnPlayerDead -= Restart;
        Enemy.StartFight -= Fight;
        Enemy.EnemyDie -= Play;
        NPC.OpenDialogMenu -= OpenDialog;
        Dialog.EndDialog -= Play;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (Menu == MenuName.Play || Menu == MenuName.Pause))
        {
            if (Menu == MenuName.Play)
            {
                Pause();
            }
            else if (Menu == MenuName.Pause)
            {
                Play();
            }
        }
    }

    #region Menu
    private void Play()
    {
        Menu = MenuName.Play;
        OnPlayed?.Invoke();
        HUD.SetActive(true);
        StaticInterface.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        RestartMenu.SetActive(false);
        FightMenu.SetActive(false);
        DialogMenu.SetActive(false);
    }

    private void Pause()
    {
        Menu = MenuName.Pause;
        OnPaused?.Invoke();
        HUD.SetActive(false);
        StaticInterface.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(true);
        RestartMenu.SetActive(false);
        FightMenu.SetActive(false);
        DialogMenu.SetActive(false);
    }

    private void Restart()
    {
        Menu = MenuName.Restart;
        OnRestart?.Invoke();
        HUD.SetActive(false);
        StaticInterface.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        RestartMenu.SetActive(true);
        FightMenu.SetActive(false);
        DialogMenu.SetActive(false);
    }

    private void Fight()
    {
        Menu = MenuName.Fight;
        OnFight?.Invoke();
        HUD.SetActive(true);
        StaticInterface.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        RestartMenu.SetActive(false);
        FightMenu.SetActive(true);
        DialogMenu.SetActive(false);
    }

    private void OpenDialog()
    {
        Menu = MenuName.Dialog;
        OnDialog?.Invoke();
        HUD.SetActive(false);
        StaticInterface.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        RestartMenu.SetActive(false);
        FightMenu.SetActive(false);
        DialogMenu.SetActive(true);
    }

    private void BackToMainMenu()
    {
        Menu = MenuName.Main;
        OnMain?.Invoke();
        HUD.SetActive(false);
        StaticInterface.SetActive(true);
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        RestartMenu.SetActive(false);
        FightMenu.SetActive(false);
        DialogMenu.SetActive(false);
    }
    #endregion

    #region Button
    public void OnButtonClick(string button)
    {
        if (button == "Demo")
        {
            SceneManager.LoadScene("Demo");
            Play();
        }
        if (button == "Play")
        {
            Menu = MenuName.Play;
            Debug.Log("Загрузка Игровой сцены");
            //Play();
            //SceneManager.LoadScene("Demo");
        }
        if (button == "Pause")
        {
            if (Menu == MenuName.Play)
            {
                Pause();
            }
            else if (Menu == MenuName.Pause)
            {
                Play();
            }
        }
        if (button == "Restart")
        {
            Debug.Log("Загрузка Последнего сохранения");
            Play();
            SceneManager.LoadScene("Demo");
        }
        if (button == "Back")
        {
            SceneManager.LoadScene("Menu");
        }
        if (button == "Exit")
        {
            Application.Quit();
        }
    }

    public void PlauClickSound()
    {
        GetComponent<AudioSource>().Play();
    }
    #endregion
}
