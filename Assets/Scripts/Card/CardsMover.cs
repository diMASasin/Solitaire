using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsMover : MonoBehaviour
{
    [SerializeField] private float _moveCardsDuration = 0.05f;
    [SerializeField] private float _moveCardDurationDelay = 0.05f;
    [SerializeField] private float _moveCardFromDeckDuration = 0.07f;
    [SerializeField] private float _moveCardFromDeckDelay = 0.10f;
    [SerializeField] private float _rotateCardDuration = 0.4f;

    public float MoveCardDurationDelay => _moveCardDurationDelay;
    public float MoveCardFromDeckDelay => _moveCardFromDeckDelay;


    public Tween Tween { get; private set; } = null;

    public void MoveCards(List<Card> cards)
    {
        foreach (var card in cards)
        {
            if (cards.IndexOf(card) != cards.Count - 1)
                Tween = card.transform.DOLocalMove(card.transform.localPosition + new Vector3(0.1f, 0, -0.01f), _moveCardsDuration);
        }
    }

    public void MoveCardFromDeck(Card card)
    {
        Tween = card.transform.DOLocalMove(card.transform.localPosition + new Vector3(0.2f, 0, 0), _moveCardFromDeckDuration);
    }

    public void RotateCard(Card card)
    {
        Vector3 localPosition = card.transform.localPosition;
        Tween = card.transform.DOLocalMove(new Vector3(localPosition.x + 1, 0, localPosition.z - 0.01f), _rotateCardDuration + 0.1f);
        card.transform.DORotate(new Vector3(card.transform.eulerAngles.x, 0, 180), _rotateCardDuration);
    }
}
