using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private YandexAd _yandexAd;

    public event UnityAction LevelStarted;
    public event UnityAction LevelRestarted;
    public event UnityAction LevelLost;

    private void Start() => LevelStarted?.Invoke();

    public void Restart()
    {
#if !UNITY_EDITOR
        _yandexAd.ShowBilboardAd(() => _yandexAd.StopGame(), (value) =>
        {
            _yandexAd.StartGame();
            LevelRestarted?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
#else
        LevelRestarted?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }

    public void Lose()
    {
        LevelLost?.Invoke();
    }
}