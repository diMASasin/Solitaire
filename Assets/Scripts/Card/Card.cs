using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardValues _valueName;
    [SerializeField] private CardSuits _suit;
    [SerializeField] private int _value;

    public CardValues ValueName => _valueName;
    public CardSuits Suit => _suit;
    public int Value => _value;

    private void OnValidate()
    {
        if ((int)_valueName <= 11)
            _value = (int)_valueName;
        else
            _value = 10;
    }
}