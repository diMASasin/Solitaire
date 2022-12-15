using UnityEngine;
using TMPro;

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

        Debug.Log(TotalValue);
        _text.text = "—чет:" + _value.ToString();

        if (_value > TotalValue)
            TotalValue = _value;

    }
}
