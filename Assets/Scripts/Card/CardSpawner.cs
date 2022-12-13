using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] Deck _deck;
    [SerializeField] Transform _spawnPosition;
    [SerializeField] private int _maxCards = 8;
    [SerializeField] private CardsMover _cardMover;

    private List<Card> _spawnedCards = new List<Card>();
    private Card _showingCard;

    public Card ShowingCard => _showingCard;

    private IEnumerator Start()
    {
        for (int i = 0; i < _maxCards; i++)
        {
            SpawnCard();
            _cardMover.MoveCards(_spawnedCards);
            yield return _cardMover.Tween?.WaitForCompletion();
            yield return new WaitForSeconds(0.2f);
        }
        yield return _cardMover.Tween?.WaitForCompletion();

        ShowFirstCard();
    }

    public void DestroyCard()
    {
        if (!_showingCard)
            return;

        Destroy(_showingCard.gameObject);
        ShowFirstCard();
    }

    public void ShowFirstCard()
    {
        _showingCard = _spawnedCards[0];
        SpawnCard();
        _cardMover.MoveCardFromDeck(_showingCard);
        _cardMover.Tween.OnComplete(() => StartCoroutine(DelayedRotateCard()));
    }

    private IEnumerator DelayedRotateCard()
    {
        yield return new WaitForSeconds(0.3f);
        _cardMover.RotateCard(_showingCard);
        _cardMover.Tween.OnComplete(() =>
        {
            _cardMover.MoveCards(_spawnedCards);
            _showingCard.Dragger.SetCanDrag(true);
        });
        _spawnedCards.Remove(_showingCard);
    }

    private void SpawnCard()
    {
        if (_deck.TryGetRandomCard(out Card card))
        {
            var newCard = Instantiate(card, _spawnPosition.position, Quaternion.Euler(90, 0, 0), _spawnPosition);
            _spawnedCards.Add(newCard);
        }
    }
}