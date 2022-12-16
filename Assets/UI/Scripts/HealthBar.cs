using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private List<Heart> _hearts;
    [SerializeField] private Level _level;
    [SerializeField] private YandexAd _yandexAd;
    [SerializeField] private Score _score;
    [SerializeField] private RestartPanel _restartPanel;
    [SerializeField] private RestartPanel _gameoverPanel;
    [SerializeField] private UIFakeCard _fakeCard;
    [SerializeField] private YandexLeaderboard _leaderboard;

    private int _index;
    private bool _rewarnPanelShown = false;
    
    private const int MaxIndex = 2;

    private void Start() => _index = 0;

    public void RemoveHeart()
    {
        _hearts[_index].MoveToStartPosition();

        if (_rewarnPanelShown == true) 
        {
            _restartPanel.Init(this, _level);

            _gameoverPanel.gameObject.SetActive(true);
            _fakeCard.gameObject.SetActive(true);

            _leaderboard.SetLeaderboardScore(_score.TotalValue); 

            return;
        }

        if (_index == 2) 
        {
            _restartPanel.gameObject.SetActive(true);
            _rewarnPanelShown = true;
            _fakeCard.gameObject.SetActive(true);
            _restartPanel.Init(this, _level);
            _leaderboard.SetLeaderboardScore(_score.TotalValue); 
        }

        _index++;
    }

    public void AddHeart()
    {
        _index = 2;

        _hearts[MaxIndex].ReturnDefaultState();
    }
}
