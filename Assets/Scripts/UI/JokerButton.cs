using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class JokerButton : MonoBehaviour
{
    [SerializeField] private JokerGiver _jokerGiver;
    [SerializeField] private Button _button;
    [SerializeField] private YandexAd _yandexAd;
    [SerializeField] private GameObject _textWithAd;
    [SerializeField] private GameObject _textWithoutAd;

    private bool _withAd = true;

    private void Start()
    {
        Disable();
    }

    private void OnEnable() => _button.onClick.AddListener(OnButtonClicked);

    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClicked);

    private void OnButtonClicked()
    {
        if (_withAd)
        {
#if !UNITY_EDITOR
        _yandexAd.ShowVideoAd(() =>
        {
            GiveJoker();
        });
#else
            GiveJoker();
            Debug.Log("VideoShowed");
#endif
        }
        else
        {
            GiveJoker();
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void EnableWithAd()
    {
        _withAd = true;
        gameObject.SetActive(true);
        _textWithAd.SetActive(true);
        _textWithoutAd.SetActive(false);
    }

    public void EnableWithoutAd()
    {
        _withAd = false;
        gameObject.SetActive(true);
        _textWithAd.SetActive(false);
        _textWithoutAd.SetActive(true);
    }

    private void GiveJoker()
    {
        _jokerGiver.GiveJoker();
        Disable();
    }
}
