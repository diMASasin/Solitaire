using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SumPointsInColumn : MonoBehaviour
{
    [SerializeField] private Column _column;

    private TextMeshProUGUI _text;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    private void OnEnable() => _column.PointsChanged += OnPointChanged;

    private void OnDisable() => _column.PointsChanged -= OnPointChanged;

    private void OnPointChanged(int value)
    {
        if (_column.CardsCount == 1 && _column.FirstCard.ValueName == CardValues.Ace)
        {
            var ace = _column.FirstCard as TwoValuesCard;
            _text.text = ace.SecondValue + "/";
            _text.text += _column.FirstCard.Value.ToString();
            return;
        }

        _text.text = value.ToString();
    }
}
