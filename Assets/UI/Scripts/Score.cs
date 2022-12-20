using UnityEngine;
using TMPro;
using Lean.Localization;


public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _value;

    private const string TOTAL_VALUE = nameof(TOTAL_VALUE);

    public int TotalValue
    {
        get { return PlayerPrefs.GetInt(TOTAL_VALUE); }
        private set { PlayerPrefs.SetInt(TOTAL_VALUE, value); }
    }

    public int Value => _value;

    private const int Revard = 150;

    public void AddScore() 
    {
        _value += Revard;

        _text.text = LeanLocalization.CurrentTranslations["����"].Data.ToString() + ": " + _value.ToString();

        //_text.text = "����:" + _value.ToString();

        if (_value > TotalValue)
            TotalValue = _value;
    }
}
