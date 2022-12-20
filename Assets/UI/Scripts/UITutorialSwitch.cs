using UnityEngine;
using UnityEngine.UI;

public class UITutorialSwitch : MonoBehaviour
{
    [SerializeField] private Button _thisButton;
    [SerializeField] private Button _secondButton;

    [SerializeField] private GameObject _firstPanel;
    [SerializeField] private GameObject _secondPanel;

    [SerializeField] private Image _thisPoint;
    [SerializeField] private Image _secondPoint;
    [SerializeField] private Sprite _filledPointSprite;
    [SerializeField] private Sprite _emptyPointSprite;


    private void OnEnable() => _thisButton.onClick.AddListener(OnButtonClick);


    private void OnDisable() => _thisButton.onClick.AddListener(OnButtonClick);

    private void OnButtonClick()
    {
        _thisButton.gameObject.SetActive(false);
        _secondButton.gameObject.SetActive(true);

        _firstPanel.gameObject.SetActive(false);
        _secondPanel.gameObject.SetActive(true);

        _thisPoint.sprite = _filledPointSprite;
        _secondPoint.sprite = _emptyPointSprite;
    }
}
