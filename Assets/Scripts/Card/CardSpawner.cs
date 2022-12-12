using System.Collections.Generic;
using UnityEngine;
using System;

public class CardSpawner : MonoBehaviour
{
  [SerializeField] Deck _deck;
  [SerializeField] Transform _spawnPosition;
  [SerializeField] private int _maxCards = 7;

  private List<Card> _spawnedCards = new List<Card>();

  private void Start()
  {
    for (int i = 0; i <= _maxCards; i++)
      SpawnCard();
    RotateFirstCard();
  }

  public Card GetCard()
  {
    if (_spawnedCards.Count == 0)
      throw new ArgumentOutOfRangeException();

    var card = _spawnedCards[0];
    _spawnedCards.Remove(card);
    SpawnCard();
    RotateFirstCard();
    return card;
  }

  public void DestroyCard()
  {
    Destroy(GetCard().gameObject);
  }

  private void SpawnCard()
  {
    if (_spawnedCards.Count >= _maxCards)
      return;

    if (_deck.TryGetRandomCard(out Card card))
    {
      var newCard = Instantiate(card, _spawnPosition.position, Quaternion.Euler(90, 0, 0), _spawnPosition);
      _spawnedCards.Add(newCard);
    }

    MoveCards();
  }

  private void MoveCards()
  {
    foreach (var card in _spawnedCards)
      card.transform.localPosition += new Vector3(0.1f, 0, -0.01f);
  }

  private void RotateFirstCard()
  {
    if (_spawnedCards.Count == 0)
      return;

    var card = _spawnedCards[0];
    card.transform.localPosition += new Vector3(1, 0, 0);
    card.transform.rotation = Quaternion.Euler(270, 0, 0);
  }
}