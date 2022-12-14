using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;
using Lean.Localization;

public class YandexInitialize : MonoBehaviour
{
    private const int FirstSceneIndex = 1;
    private void Start() => LoadScene();
    private void LoadScene() => StartCoroutine(SDKInitialized());

    private IEnumerator SDKInitialized()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize(() => PlayerAccount.RequestPersonalProfileDataPermission());

        string language = YandexGamesSdk.Environment.i18n.lang;
        LeanLocalization.SetCurrentLanguageAll(language);

        SceneManager.LoadScene(FirstSceneIndex);
    }
}
