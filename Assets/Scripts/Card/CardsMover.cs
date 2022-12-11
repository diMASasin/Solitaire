using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsMover : MonoBehaviour
{
    public Tween Tween { get; private set; } = null;

    public void MoveCards(List<Card> cards)
    {
        foreach (var card in cards)
        {
            if (cards.IndexOf(card) != cards.Count - 1)
                Tween = card.transform.DOLocalMove(card.transform.localPosition + new Vector3(0.1f, 0, -0.01f), 0.15f);
        }
    }

    public void MoveCardFromDeck(Card card)
    {
        Tween = card.transform.DOLocalMove(card.transform.localPosition + new Vector3(0.2f, 0, 0), 0.15f);
    }

    public void RotateCard(Card card)
    {
        Tween = card.transform.DOLocalMoveX(card.transform.localPosition.x + 1, 0.8f);
        card.transform.DORotate(new Vector3(card.transform.eulerAngles.x, 0, 180), 0.7f);
    }
}
