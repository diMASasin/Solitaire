using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _value;

    public int Value => _value;

    private const int Revard = 150;

    public void AddScore()
    {
        _value += Revard;
        _text.text = "—чет:" + _value.ToString();
    }
}
