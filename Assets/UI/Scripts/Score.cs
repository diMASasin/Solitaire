using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private const int Revard = 150;

    [SerializeField] private TextMeshProUGUI _text;

    private int _value;

    private const string TOTAL_VALUE = nameof(TOTAL_VALUE);

    public int TotalValue
    {
        get { return PlayerPrefs.GetInt(TOTAL_VALUE); }
        private set { PlayerPrefs.SetInt(TOTAL_VALUE, value); }
    }

    public void AddScore()
    {
        _value += Revard;
        TotalValue += Revard;
        Debug.Log(TotalValue);
        _text.text = "—чет:" + _value.ToString();
    }
}
