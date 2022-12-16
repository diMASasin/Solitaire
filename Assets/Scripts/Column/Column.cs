using System;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private const int _maxValue = 21;

    [SerializeField] private List<Transform> _points = new();
    [SerializeField] private CardSpawner _cardSpawner;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Score _score;

    private List<Card> _cards = new();
    private bool _readyToAccept;
    private int _index = 0;
    private int _currentValue = 0;

    public int CardsCount => _cards.Count;
    public Card FirstCard => _cards[0];

    public bool IsMaxValueReached { get; private set; } = false;

    public event Action MaxValueReachedChanged;
    public event Action<int> PointsChanged;

    private void Start() => PointsChanged?.Invoke(_currentValue);

    private void OnMouseEnter() => _readyToAccept = true;

    private void OnMouseExit() => _readyToAccept = false;

    private void Reset()
    {
        _index = 0;
        _currentValue = 0;
        PointsChanged?.Invoke(_currentValue);

        foreach (Card item in _cards)
            item.gameObject.SetActive(false);

        _cards.Clear();
    }

    public void AddNewCard(Card card)
    {
        _cards.Add(card);
        card.transform.position = transform.position;
        card.Move(_points[_index].position);
        _cardSpawner.ShowingCard.Dragger.SetCanDrag(false);
        _cardSpawner.ShowFirstCard();
        _cardSpawner.SpawnCard();

        if (_index == _points.Count)
            return;

        _index++;

        if (card.ValueName == CardValues.Joker)
            _currentValue = _maxValue;
        else
            _currentValue += card.Value;

        PointsChanged?.Invoke(_currentValue);
        print(_currentValue);


        TryChangeAcePoints();

        if (_currentValue == _maxValue)
        {
            _score.AddScore();
            if (card.ValueName != CardValues.Joker)
            {
                IsMaxValueReached = true;
                MaxValueReachedChanged?.Invoke();
            }
        }

        if (_currentValue > _maxValue)
            _healthBar.RemoveHeart();

        if (_currentValue >= _maxValue)
            Reset();
    }

    public void ResetMaxValueReached()
    {
        IsMaxValueReached = false;
        MaxValueReachedChanged?.Invoke();
    }

    private void TryChangeAcePoints()
    {
        if (_currentValue > _maxValue)
        {
            foreach (var card1 in _cards)
            {
                if (card1.ValueName == CardValues.Ace)
                {
                    var ace = card1 as TwoValuesCard;
                    if (!ace.IsSecondValueUsing)
                    {
                        ace.IsSecondValueUsing = true;
                        break;
                    }
                }
            }

            _currentValue = CalculateSum();
            PointsChanged?.Invoke(_currentValue);
        }
    }

    private int CalculateSum()
    {
        int sum = 0;
        foreach (var card in _cards)
            sum += card.Value;

        Debug.Log($"CalculateSum: {sum}");
        return sum;
    }
}