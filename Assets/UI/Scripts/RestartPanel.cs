using UnityEngine;
using Agava.YandexGames;
using System;
using UnityEngine.UI;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private UITimeHandler _timeHandler;
    [SerializeField] private Button _button;
    [SerializeField] private UIFakeCard _fakeCard;

    private HealthBar _healthBar;
    private Level _level;
        
    private void Start() => _timeHandler.StartTimer(OnClose);

    public void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnErrorCallback();
#endif
        //вырубить музыку
        VideoAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
    }

    public void RestartLevel() => _level.Restart();

    public void Init(HealthBar healthBar, Level level)
    {
        _healthBar = healthBar;
        _level = level;
    }

    private void OnOpenCallback() => _healthBar.AddHeart(); //Где включается панель, дающая жизнь?? И отключается

    private void OnCloseCallback()
    {
        _fakeCard.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private void OnErrorCallback()
    {
        _healthBar.AddHeart();
        _fakeCard.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private void OnClose()
    {
        _fakeCard.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
}