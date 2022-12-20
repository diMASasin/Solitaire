using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SumPointsInColumn : MonoBehaviour
{
    [SerializeField] private Column _column;

    private TextMeshProUGUI _text;
    private Sequence _tween;

    private float _delayColorChange = 0.5f;

    public float DelayColorChange => _delayColorChange;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _tween = DOTween.Sequence();
    }

    private void OnEnable() => _column.PointsChanged += OnPointChanged;

    private void OnDisable() => _column.PointsChanged -= OnPointChanged;

    public void ChangeColor(Color color) => _tween.Append(_text.DOColor(color, _delayColorChange).OnComplete(SetStartColor));

    private void SetStartColor() => _text.color = Color.white;

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
