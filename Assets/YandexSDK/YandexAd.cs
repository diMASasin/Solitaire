using UnityEngine;
using Agava.YandexGames;
using System;

public class YandexAd : MonoBehaviour
{
    [SerializeField] Sounds _sounds;
    [SerializeField] TriesCounter _triesCounter;

    private void Start()
    {
        ShowBilboardAd(StopGame, StartGame);
    }

    private void Authorize()
    {
#if !UNITY_EDITOR
        PlayerAccount.Authorize();
#endif
    }

    public void ShowBilboardAd(Action OnOpenCallback, Action<bool> OnCloseCallback)
    {
#if !UNITY_EDITOR
        InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
#else
        Debug.Log("InterstitialShowed");
#endif
    }
    public void ShowVideoAd(Action OnRewarded) => VideoAd.Show(StopGame, OnRewarded, StartGame);

    public void StopGame()
    {
        _sounds.SwitchSounds(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _sounds.Load();
        Time.timeScale = 1;
    }

    private void StartGame(bool _)
    {
        StartGame();
    }
}
