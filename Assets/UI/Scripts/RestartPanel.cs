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

  private HealthBar _healthBar;

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

  private void OnOpenCallback() => _healthBar.AddHeart(); 

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

    if (_rewardButton != null)
    {
      _circle.gameObject.SetActive(false);
      _rewardButton.gameObject.SetActive(false);
      _skipButton.gameObject.SetActive(true);
    }
  }
}