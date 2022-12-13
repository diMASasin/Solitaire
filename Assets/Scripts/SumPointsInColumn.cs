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

    private void OnPointChanged(int value) => _text.text = value.ToString();
}
