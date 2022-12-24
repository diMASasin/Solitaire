//using System.Collections.Generic;
//using GameAnalyticsSDK;
//using UnityEngine;

//public class IntegrationMetric : MonoBehaviour
//{
//    [SerializeField] private Level _level;

//    private const string SessionCountName = "sessionCount";
//    private int _totalSpentMoney
//    {
//        get { return PlayerPrefs.GetInt(nameof(_totalSpentMoney), 0); }
//        set { PlayerPrefs.SetInt(nameof(_totalSpentMoney), _totalSpentMoney); }
//    }

//    public int SessionCount;

//    private void OnValidate()
//    {
//        _level = FindObjectOfType<Level>();
//    }

//    private void Awake()
//    {
//        GameAnalytics.Initialize();
//    }

//    private void OnEnable()
//    {
//        _level.LevelStarted += OnLevelStart;
//        _level.LevelLost += OnLevelFail;
//    }

//    private void OnDisable()
//    {
//        _level.LevelStarted -= OnLevelStart;
//        _level.LevelLost -= OnLevelFail;
//    }

//    public void OnLevelStart()
//    {
//        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level-1");
//    }

//    public void OnLevelComplete()
//    {
//        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level-1");
//    }

//    public void OnLevelFail()
//    {
//        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level-1");
//    }

//    public void OnRewardedAdStarted()
//    {
//        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "rewarded_start", "revive_button");
//    }

//    public void OnRewardedAdEnded()
//    {
//        GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "rewarded_shown", "revive_button");
//    }

//    public void OnInterstitialShown()
//    {
//        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "interstitial_start", "level start");
//    }

//    public void OnSoftSpent(string itemType, string codeName, int price)
//    {
//        Dictionary<string, object> totalSpentMoney = new Dictionary<string, object>() { { "TotalSpentMoney", _totalSpentMoney } };
//        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Money", price, itemType, codeName, totalSpentMoney);
//    }

//    private Dictionary<string, object> CreateLevelProperty(int levelIndex)
//    {
//        Dictionary<string, object> level = new Dictionary<string, object>();
//        level.Add("level", levelIndex);

//        return level;
//    }

//    private int CountSession()
//    {
//        int count = 1;

//        if (PlayerPrefs.HasKey(SessionCountName))
//        {
//            count = PlayerPrefs.GetInt(SessionCountName);
//            count++;
//        }

//        PlayerPrefs.SetInt(SessionCountName, count);
//        SessionCount = count;

//        return count;
//    }
//}