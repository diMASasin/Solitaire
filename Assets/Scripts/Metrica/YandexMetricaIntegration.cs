using Agava.YandexMetrica;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class YandexMetricaIntegration : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private RestartPanel _restartPanel;
    [SerializeField] private JokerGiver _jokerGiver;
    [SerializeField] private FirstGameTutorial _tutorial;
    [SerializeField] private YandexAd _yandexAd;

    private void OnValidate()
    {
        _level = FindObjectOfType<Level>();
        _jokerGiver = FindObjectOfType<JokerGiver>();
    }

#if !UNITY_EDITOR
    private void OnEnable()
    {
        _tutorial.LevelStarted += OnLevelStart;
        _level.LevelLost += OnLevelFail;
        _level.LevelRestarted += OnRestartLevel;
        _restartPanel.AddHeartAdOffer += OnAddHeartAdOffer;
        _restartPanel.AddHeartAdClick += OnAddHeartAdClick;
        _jokerGiver.JokerButtonEnabled += JokerAdOffer;
        _jokerGiver.JokerAdButtonClicked += JokerAdClick;
        _tutorial.TutorialStarted += OnTutorialStarted;
        _tutorial.TutorialEnd += OnTutorialEnd;
        _yandexAd.InterstitialShowed += OnInterstitialShowed;
    }

    private void OnDisable()
    {
        _tutorial.LevelStarted -= OnLevelStart;
        _level.LevelLost -= OnLevelFail;
        _level.LevelRestarted -= OnRestartLevel;
        _restartPanel.AddHeartAdOffer -= OnAddHeartAdOffer;
        _restartPanel.AddHeartAdClick -= OnAddHeartAdClick;
        _jokerGiver.JokerButtonEnabled -= JokerAdOffer;
        _jokerGiver.JokerAdButtonClicked -= JokerAdClick;
        _tutorial.TutorialStarted -= OnTutorialStarted;
        _tutorial.TutorialEnd -= OnTutorialEnd;
        _yandexAd.InterstitialShowed -= OnInterstitialShowed;
    }

    public void OnLevelStart()
    {
        YandexMetrica.Send($"levelStart");
    }

    public void OnLevelFail()
    {
        YandexMetrica.Send($"levelFail", $"{{\"Fail Time\": \"{Time.timeSinceLevelLoad}\"}}");
    }

    public void OnRestartLevel()
    {
        YandexMetrica.Send($"levelRestart");
    }

    public void OnAddHeartAdOffer()
    {
        YandexMetrica.Send($"addHeartAdOffer");
    }

    public void OnAddHeartAdClick()
    {
        YandexMetrica.Send($"addHeartAdClick");
    }

    public void JokerAdOffer()
    {
        YandexMetrica.Send($"jokerAdOffer");
    }

    public void JokerAdClick()
    {
        YandexMetrica.Send($"jokerAdClick");
    }

    public void OnTutorialStarted()
    {
        YandexMetrica.Send("tutorialStarted");
    }

    public void OnTutorialEnd()
    {
        YandexMetrica.Send("tutorialEnd");
    }

    public void OnInterstitialShowed(int count)
    {
        YandexMetrica.Send("interstitialShowed", $"{{\"Interstitial showing count\": \"{count}\"}}");
    }
#endif
}
