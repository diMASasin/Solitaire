using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private const int MaxValue = 21;

    [SerializeField] private List<Transform> _points = new();
    [SerializeField] private CardSpawner _cardSpawner;

    private List<Card> _cards = new();
    private bool _readyToAccept;
    private int _index = 0;
    private int _currentValue = 0;

    private void OnMouseEnter()
    {
        _readyToAccept = true;
    }

    private void OnMouseExit()
    {
        _readyToAccept = false;
    }

    public void AddNewCard(Card card)
    {
        _cards.Add(card);
        card.transform.position = transform.position;
        card.Move(_points[_index].position);
        _cardSpawner.GetCard().Dragger.SetCanDrag(false);

        if (_index == _points.Count)
            return;

        _index++;

        _currentValue += card.Value;
        print(_currentValue);

        if (_currentValue > 21)
        {
            foreach (Card item in _cards)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}