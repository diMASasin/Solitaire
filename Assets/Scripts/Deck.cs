using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Card[] _fullDeck;

    private List<Card> _currentDeck;

    private void Awake()
    {
        RefreshDeck();
    }

    public bool TryGetRandomCard(out Card card)
    {
        card = null;
        if (_currentDeck.Count == 0)
            return false;

        card = _currentDeck[Random.Range(0, _currentDeck.Count)];
        _currentDeck.Remove(card);

        if(_currentDeck.Count == 0)
            RefreshDeck();

        return true;
    }

    private void RefreshDeck()
    {
        _currentDeck = _fullDeck.ToList();
    }
}
