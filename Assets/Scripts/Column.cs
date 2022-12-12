using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
  private bool _readyToAccept;

  private List<CardDrag> _cards = new();

  private void OnMouseEnter()
  {
    _readyToAccept = true;
  }

  private void OnMouseExit()
  {
    _readyToAccept = false;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent(out CardDrag cardDrag) && _readyToAccept == true)
    {
      AddNewCard(cardDrag);
    }
  }

  public void AddNewCard(CardDrag card)
  {
    _cards.Add(card);
    card.transform.position = transform.position;
    card.MoveToPoint(transform.position);
  }
}