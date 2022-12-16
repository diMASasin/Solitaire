#pragma warning disable

using System;
using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YandexLeaderboard : MonoBehaviour
{
    [SerializeField] private PlayerRankingView _rankingViewPrefab;
    [SerializeField] private PlayerRankingView _playerRank;
    [SerializeField] private Transform _container;
    [SerializeField] private AutorizePanel _autorizePanel;

    private int _rankingMaxCount = 5;

    private const string LeaderboardName = "Leaderboard";

    public void SetLeaderboardScore(int value) => Leaderboard.SetScore(LeaderboardName, value);

    public void Show() //В шоу добавить разделение авторизован/али нет
    {
        //AuthtirizeIfNeed();

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
                playerRankingView.Initialize(entry.rank.ToString(), name, entry.score.ToString());

                rankingCount++;

                if (rankingCount == _rankingMaxCount)
                    break;
            }
        });
    }

    private void AuthtirizeIfNeed()
    {
        if (PlayerAccount.IsAuthorized == false && PlayerAccount.HasPersonalProfileDataPermission == false)
        {
            //if (PlayerAccount.IsAuthorized == false)
            //{
            //    PlayerAccount.Authorize(OnSucces);
            //}
            if (PlayerAccount.HasPersonalProfileDataPermission == false)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
            }

            void OnSucces()
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
            }
        }
    }
}
