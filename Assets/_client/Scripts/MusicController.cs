using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public enum MenuName
    {
        Menu,
        Play,
        Fight,
        Dialog
    }

    public static MusicController Instance;

    [SerializeField] private List<AudioClip> MenuClipList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> FightClipList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> LocationClipList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> DialogClipList = new List<AudioClip>();

    private AudioSource musicPlayer;
    private int musicIndex = 0;

    [SerializeField] private MenuName Menu;

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

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();

        MenuController.OnMain += MenuSound;
        MenuController.OnPlayed += PlayMusic;
        MenuController.OnPaused += MenuSound;
        MenuController.OnRestart += MenuSound;
        MenuController.OnFight += FightMusic;
        MenuController.OnDialog += DialogMusic;

        musicPlayer.ignoreListenerPause= true;

        musicPlayer.clip = MenuClipList[musicIndex = (musicIndex + 1) % MenuClipList.Count];
        musicPlayer.Play();
    }

    private void OnDisable()
    {
        MenuController.OnMain -= MenuSound;
        MenuController.OnPlayed -= PlayMusic;
        MenuController.OnPaused -= MenuSound;
        MenuController.OnRestart -= MenuSound;
        MenuController.OnFight -= FightMusic;
        MenuController.OnDialog -= DialogMusic;
    }

    private void DialogMusic()
    {
        StartCoroutine(CrossFade(DialogClipList[musicIndex = (musicIndex + 1) % DialogClipList.Count], 1.0f));
    }

    private void FightMusic()
    {
        StartCoroutine(CrossFade(FightClipList[musicIndex = (musicIndex + 1) % FightClipList.Count], 1.0f));
    }

    private void PlayMusic()
    {
        StartCoroutine(CrossFade(LocationClipList[musicIndex = (musicIndex + 1) % LocationClipList.Count], 1.0f));
    }

    public void MenuSound()
    {
        StartCoroutine(CrossFade(MenuClipList[musicIndex = (musicIndex + 1) % MenuClipList.Count], 1.0f));
    }

    private IEnumerator CrossFade(AudioClip nextClip, float fadeDuration)
    {
        float fadeTimer = 0f;
        float startVolume = 1.0f;
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.fixedDeltaTime;
            musicPlayer.volume = Mathf.Lerp(startVolume, 0f, fadeTimer / fadeDuration); // плавное затухание текущего трека
            yield return null;
        }

        musicPlayer.clip = nextClip;
        musicPlayer.Play();

        fadeTimer = 0f;
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.fixedDeltaTime;
            musicPlayer.volume = Mathf.Lerp(0f, startVolume, fadeTimer / fadeDuration); // плавное нарастание следующего трека
            yield return null;
        }
        musicPlayer.volume = startVolume;
    }
}
