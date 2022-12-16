using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AutorizationButton : MonoBehaviour
{
    [SerializeField] private Button _autorizationButton;

    private void OnEnable() => _autorizationButton.onClick.AddListener(OnButtonClick);

    private void OnDisable() => _autorizationButton.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick()
    {
        PlayerAccount.Authorize(OnSucces);

        void OnSucces() => PlayerAccount.RequestPersonalProfileDataPermission();
    }
}
