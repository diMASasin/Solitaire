using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;

public class LocalizationChanger : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private void Awake()
    {
        ChangeLocalization();
    }

    private void ChangeLocalization()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "en":
                _leanLocalization.CurrentLanguage = "English";
                break;
            case "tr":
                _leanLocalization.CurrentLanguage = "Turkish";
                break;
            case "ru":
                _leanLocalization.CurrentLanguage = "Russia";
                break;
            default:
                _leanLocalization.CurrentLanguage = "English";
                Debug.Log("Unknown domain");
                break;
        }
#endif
    }
}
