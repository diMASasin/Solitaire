using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoValuesCard : Card
{
    [SerializeField] private int _secondValue;

    [HideInInspector] public bool IsSecondValueUsing = false;

    public override int Value => IsSecondValueUsing ? _secondValue : base.Value;
}
