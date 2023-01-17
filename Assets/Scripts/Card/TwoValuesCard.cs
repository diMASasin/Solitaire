using UnityEngine;

public class TwoValuesCard : Card
{
    [SerializeField] private int _secondValue;

    [HideInInspector] public bool IsSecondValueUsing = false;

    public int SecondValue => _secondValue;
    public override int Value => IsSecondValueUsing ? _secondValue : base.Value;

    private void Awake()
    {
        IsSecondValueUsing = false;
    }
}
