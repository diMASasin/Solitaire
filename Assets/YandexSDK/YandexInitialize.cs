using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;
using Lean.Localization;

public class YandexInitialize : MonoBehaviour
{
    [SerializeField] private TriesCounter _triesCounter;

    private const int FirstSceneIndex = 1;

    private void Start()
    {
        _triesCounter.Reset();
        LoadScene();
    }

    private void LoadScene() => StartCoroutine(SDKInitialized());

    private IEnumerator SDKInitialized()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        SceneManager.LoadScene(FirstSceneIndex);
        yield break;
#endif
        yield return YandexGamesSdk.Initialize(() => PlayerAccount.RequestPersonalProfileDataPermission());

        string language = YandexGamesSdk.Environment.i18n.lang;
        LeanLocalization.SetCurrentLanguageAll(language);

        SceneManager.LoadScene(FirstSceneIndex);
    }
}
