using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class JokerButton : MonoBehaviour
{
    [SerializeField] private JokerGiver _jokerGiver;
    [SerializeField] private Button _button;
    [SerializeField] private YandexAd _yandexAd;

    private void Start()
    {
        Disable();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
#if !UNITY_EDITOR
        _yandexAd.ShowVideoAd(() =>
        {
            _jokerGiver.GiveJoker();
            Disable();
        });
#else
        _jokerGiver.GiveJoker();
        Disable();
#endif

    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
