using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private YandexAd _yandexAd;

    public event UnityAction LevelStarted;
    public event UnityAction LevelRestarted;
    public event UnityAction LevelLost;

    public void Restart()
    {
        LevelRestarted?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Lose()
    {
        LevelLost?.Invoke();
    }
}