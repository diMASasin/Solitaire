using UnityEngine;
using TMPro;
using Lean.Localization;


public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _value;

    private const string RECORD = nameof(RECORD);
    public int Record
    {
        get { return PlayerPrefs.GetInt(RECORD); }
        private set { PlayerPrefs.SetInt(RECORD, value); }
    }

    public int Value => _value;

    private const int Revard = 150;

    private void Start()
    {
        _text.text = LeanLocalization.CurrentTranslations["Очки"].Data.ToString() + ": " + _value.ToString();
    }

    public void AddScore() 
    {
        _value += Revard;

        _text.text = LeanLocalization.CurrentTranslations["Очки"].Data.ToString() + ": " + _value.ToString();

        if (_value > Record)
            Record = _value;
    }
}
