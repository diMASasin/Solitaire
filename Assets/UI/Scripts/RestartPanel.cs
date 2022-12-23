using UnityEngine;
using Agava.YandexGames;
using UnityEngine.UI;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private UITimeHandler _timeHandler;
    [SerializeField] private UIFakeCard _fakeCard;
    [SerializeField] private Button _skipButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Level _level;
    [SerializeField] private Image _circle;
    [SerializeField] private YandexAd _yandexAd;

    private HealthBar _healthBar;

    private void Start() => _timeHandler.StartTimer(OnClose);

    public void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnRewardedCallback();
#endif
        VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback);
    }

    public void RestartLevel() => _level.Restart();

    public void Init(HealthBar healthBar, Level level)
    {
        _healthBar = healthBar;
        _level = level;
    }

    private void OnOpenCallback()
    {
        _yandexAd.StopGame();
    }

    private void OnCloseCallback()
    {
        _yandexAd.StartGame();

        _fakeCard.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnRewardedCallback()
    {
        _healthBar.AddHeart();
        _fakeCard.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private void OnClose()
    {
        _fakeCard.gameObject.SetActive(false);

        if (_rewardButton != null)
        {
            _level.Restart();
        }
    }
}