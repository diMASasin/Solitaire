using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSounds : MonoBehaviour
{
    [SerializeField] private CardDrag _cardDrag;
    [SerializeField] private AudioSource _takeCard;
    [SerializeField] private AudioSource _dropCard;

    private void OnValidate()
    {
        _cardDrag = GetComponent<CardDrag>();
    }

    private void OnEnable()
    {
        _cardDrag.CardTook += PlayTakeCardSound;
        _cardDrag.CardDroped += PlayDropCardSound;
    }

    private void OnDisable()
    {
        _cardDrag.CardTook -= PlayTakeCardSound;
        _cardDrag.CardDroped -= PlayDropCardSound;
    }

    private void PlayTakeCardSound()
    {
        _takeCard.Play();
    }

    private void PlayDropCardSound()
    {
        _dropCard.Play();
    }
}
