using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
  [SerializeField] private Card[] _fullDeck;

  private List<Card> _currentDeck;

    [SerializeField] private List<Card> _tutorialDeck;
    [SerializeField] private List<Card> _secondTutorialDeck;

    //Тутор
    int indexCard = 0;
    //


    private void Awake() => RefreshDeck();

  public bool TryGetRandomCard(out Card card)
  {
    card = null;
    if (_currentDeck.Count == 0)
      return false;

        //Туториальная дека
        card = _tutorialDeck[indexCard];
        indexCard++;

        // Игровая дека
        //card = _currentDeck[Random.Range(0, _currentDeck.Count)];
        //_currentDeck.Remove(card);
        //

        if (_currentDeck.Count == 0)
      RefreshDeck();

    return true;
  }

  private void RefreshDeck() => _currentDeck = _fullDeck.ToList();
}