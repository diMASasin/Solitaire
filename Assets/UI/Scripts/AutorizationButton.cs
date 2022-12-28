using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AutorizationButton : MonoBehaviour
{
    [SerializeField] private Button _autorizationButton;
    [SerializeField] private YandexLeaderboard _leaderboard;
    [SerializeField] private AutorizePanel _autorizePanel;

    private void OnEnable() => _autorizationButton.onClick.AddListener(OnButtonClick);

    private void OnDisable() => _autorizationButton.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick()
    {
        PlayerAccount.Authorize(OnSucces);
    }

    private void OnSucces()
    {
        _leaderboard.UpdateRecord();
        _leaderboard.Show();
        PlayerAccount.RequestPersonalProfileDataPermission();
        _autorizePanel.gameObject.SetActive(false);
    }
}
