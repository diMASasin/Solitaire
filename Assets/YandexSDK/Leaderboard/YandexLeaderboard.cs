#pragma warning disable

using System;
using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YandexLeaderboard : MonoBehaviour
{
    [SerializeField] private PlayerRankingView _rankingViewPrefab;
    [SerializeField] private PlayerRankingView _playerRank;
    [SerializeField] private Transform _container;
    [SerializeField] private AutorizePanel _autorizePanel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private LeanToken _record;
    [SerializeField] private Score _score;

    private int _rankingMaxCount = 5;
    private static string _playerID = null;

    private const string LeaderboardName = "Leaderboard";

    public bool HasRecord { get; private set; } = false;

    private void Start()
    {
        UpdateRecord();
    }

    public void SetLeaderboardScore(int value)
    {
#if !UNITY_EDITOR
        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            Debug.Log($"value: {value} result.sore: {result.score}");
            if(value > result.score)
            {
                Leaderboard.SetScore(LeaderboardName, value);
                HasRecord = true;
            }
        });
#endif
    }

    public void Show() 
    {
#if UNITY_EDITOR
        return;
#endif

        if (PlayerAccount.IsAuthorized == false)
        {
            _autorizePanel.gameObject.SetActive(true);
            return;
        }
        else
            _autorizePanel.gameObject.SetActive(false);

        PlayerRankingView[] _ranking = _container.GetComponentsInChildren<PlayerRankingView>();

        foreach (var item in _ranking)
        {
            Destroy(item.gameObject);
        }

        GetLeaderboardData();
        AuthtirizeIfNeed();
    }

    public void GetLeaderboardData()
    {
        Leaderboard.GetEntries(LeaderboardName, (result) => 
        {
            int rankingCount = 0;

            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";

                PlayerRankingView playerRankingView = Instantiate(_rankingViewPrefab, _container);
                if (_playerID == null)
                    Leaderboard.GetPlayerEntry(LeaderboardName, (result) => _playerID = result.player.uniqueID);
                Debug.Log($"{entry.player.uniqueID} == {_playerID}: {entry.player.uniqueID == _playerID}");
                playerRankingView.Initialize(entry.rank.ToString(), name, entry.score.ToString(), entry.player.uniqueID == _playerID);

                rankingCount++;
            }
        });
    }

    public void EnableRestartButton()
    {
        _restartButton.gameObject.SetActive(true);
        _closeButton.gameObject.SetActive(false);
    }

    public void UpdateRecord()
    {
#if !UNITY_EDITOR
        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            _playerID = result.player.uniqueID;
            Debug.Log("PlayerID: " + _playerID);
            _record.Value = result.score.ToString();
        },
        (error) =>
        {
            _record.Value = _score.Record.ToString();
        });
#else
        _record.Value = _score.Record.ToString();
#endif
    }

    private void AuthtirizeIfNeed()
    {
        if (PlayerAccount.IsAuthorized == false && PlayerAccount.HasPersonalProfileDataPermission == false)
        {
            if (PlayerAccount.HasPersonalProfileDataPermission == false)
                PlayerAccount.RequestPersonalProfileDataPermission();

            void OnSucces()
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
            }
        }
    }
}
