using UnityEngine;
using Agava.YandexGames;
using System;

public class YandexAd : MonoBehaviour
{
    //[SerializeField] private HealthBar _healthBar;

    private void Start() => Authorize();

    private void Authorize() => PlayerAccount.Authorize();

    public void ShowBilboardAd() => InterstitialAd.Show();

    public void ShowVideoAd(Action OnRewarded) => VideoAd.Show(OnOpenCallback, OnRewarded, OnCloseCallback);

    private void OnOpenCallback()
    {
        Time.timeScale = 0;
    }

    private void OnCloseCallback()
    {
        Time.timeScale = 1;
    }
}
