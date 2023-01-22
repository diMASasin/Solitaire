using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private TMP_Text _nextText;
    [SerializeField] private RestartPanel _restartPanel;
    [SerializeField] private YandexLeaderboard _leaderboard;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if(_leaderboard.HasRecord)
        {
            _restartText.gameObject.SetActive(false);
            _nextText.gameObject.SetActive(true);
        }
        else
        {
            _restartText.gameObject.SetActive(true);
            _nextText.gameObject.SetActive(false);
        }

        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
        
    }

    private void OnButtonClicked()
    {
        _restartPanel.LoseAndEnableLeaderboard();
        _restartPanel.gameObject.SetActive(false);
    }
}
