using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] protected Deck Deck;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private int _maxCards = 7;
    [SerializeField] private CardsMover _cardMover;
    [SerializeField] private Column[] _columns; 

    private List<Card> _spawnedCards = new List<Card>();
    private Card _showingCard;
    private bool _cardsDealed = false;

    public Card ShowingCard => _showingCard;

    public event Action DealCardsStared;
    public event Action DealCardsEnd;
    public event Action MoveCardStarted;
    public event Action CardDroped;

    private void OnValidate()
    {
        _columns = FindObjectsOfType<Column>();
    }

    private void OnEnable()
    {
        foreach (var column in _columns)
            column.CardAdded += OnCardAdded;
    }

    private void OnDisable()
    {
        foreach (var column in _columns)
            column.CardAdded -= OnCardAdded;
    }

    public void StartDealCards()
    {
        if (_cardsDealed)
            return;

        _cardsDealed = true;
        StartCoroutine(DealCards());
    }

    public void DestroyCard()
    {
        if (!_showingCard)
            return;

        Destroy(_showingCard.gameObject);
    }

    public void ShowFirstCard()
    {
        _showingCard = _spawnedCards[0];
        _cardMover.MoveCardFromDeck(_showingCard);
        MoveCardStarted?.Invoke();
        _cardMover.Tween.OnComplete(() =>
        {
            if(gameObject.activeInHierarchy)
                StartCoroutine(DelayedRotateCard());
        });
    }

    public void InsertInFirst(Card card)
    {
        var pos = _spawnedCards[0].transform.localPosition;
        _spawnedCards.Insert(0, card);
        card.transform.parent = _spawnPosition;
        card.transform.localPosition = pos;
    }

    public virtual void SpawnRequiredCard()
    {
        if (Deck.TryGetRandomCard(out Card card))
            SpawnCard(card);
    }

    public void MoveSpawnedCardsBack()
    {
        _cardMover.MoveCardsBack(_spawnedCards);
    }

    public void OnCardAdded()
    {
        if (!_cardsDealed)
            return;

        if(ShowingCard && ShowingCard.isActiveAndEnabled)
            ShowingCard.Dragger.SetCanDrag(false);
        ShowFirstCard();
        SpawnRequiredCard();
    }

    protected void SpawnCard(Card card)
    {
        var newCard = Instantiate(card, _spawnPosition.position, Quaternion.Euler(90, 0, 0), _spawnPosition);
        _spawnedCards.Add(newCard);
    }

    private IEnumerator DealCards()
    {
        DealCardsStared?.Invoke();

        for (int i = 0; i < _maxCards; i++)
        {
            SpawnRequiredCard();
            _cardMover.MoveCards(_spawnedCards);
            yield return _cardMover.Tween?.WaitForCompletion();
            yield return new WaitForSeconds(_cardMover.MoveCardDurationDelay);
        }
        yield return _cardMover.Tween?.WaitForCompletion();

        DealCardsEnd?.Invoke();
        ShowFirstCard();
        SpawnRequiredCard();
    }

    private IEnumerator DelayedRotateCard()
    {
        yield return new WaitForSeconds(_cardMover.MoveCardFromDeckDelay);
        _cardMover.RotateCard(_showingCard);
        _cardMover.Tween.OnComplete(() =>
        {
            _cardMover.MoveCards(_spawnedCards);
            _showingCard.Dragger.SetCanDrag(true);
            CardDroped?.Invoke();
        });
        _spawnedCards.Remove(_showingCard);
    }
}