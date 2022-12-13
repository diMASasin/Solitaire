using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private const int Revard = 150;

    [SerializeField] private TextMeshProUGUI _text;

    private int _value;

    public void AddScore()
    {
        _value += Revard;
        _text.text = "—чет:" + _value.ToString();
    }
}
