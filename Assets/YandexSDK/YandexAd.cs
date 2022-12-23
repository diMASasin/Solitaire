using UnityEngine;
using Agava.YandexGames;
using System;

public class YandexAd : MonoBehaviour
{
    [SerializeField] Sounds _sounds;

    private void Start() => Authorize();

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
#endif
    }
    public void ShowVideoAd(Action OnRewarded) => VideoAd.Show(OnVideoOpenCallback, OnRewarded, OnVideoCloseCallback);

    public void StopGame()
    {
        _sounds.SwitchSounds(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _sounds.SwitchSounds(true);
        Time.timeScale = 1;
    }

    private void OnVideoOpenCallback()
    {
        StopGame();
    }

    private void OnVideoCloseCallback()
    {
        StartGame();
    }
}
