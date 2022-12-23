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
        LevelRestarted?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Lose()
    {
        _yandexAd.ShowBilboardAd();
        LevelLost?.Invoke();
    }
}