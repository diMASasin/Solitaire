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
    [SerializeField] private YandexLeaderboard _leaderboard;

    private HealthBar _healthBar;
    private bool _adShowing = false;

    private void Start() => _timeHandler.StartTimer(OnClose);

    public void Show()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnRewardedCallback();
        return;
#endif
        VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback);
    }

    public void RestartLevel() => _level.Restart();

    public void Init(HealthBar healthBar, Level level)
    {
        _healthBar = healthBar;
        _level = level;
    }

    public void EnableLeaderboardOrRestart()
    {
        if (_leaderboard.HasRecord)
        {
            _leaderboard.gameObject.SetActive(true);
            _leaderboard.Show();
            _leaderboard.EnableRestartButton();
        }
        else
        {
            _level.Restart();
        }
    }

    private void OnOpenCallback()
    {
        _adShowing = true;
        _yandexAd.StopGame();
    }

    private void OnCloseCallback()
    {
        _yandexAd.StartGame();

        _fakeCard.gameObject.SetActive(false);
        gameObject.SetActive(false);
        _adShowing = false;
        OnClose();
    }

    private void OnRewardedCallback()
    {
        _healthBar.AddHeart();
        _fakeCard.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private void OnClose()
    {
        if (_adShowing)
            return;

        _fakeCard.gameObject.SetActive(false);
    }
}