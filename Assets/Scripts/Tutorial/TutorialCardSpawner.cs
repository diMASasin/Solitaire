using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCardSpawner : CardSpawner
{
    private bool _canSpawnCards = true;

    public override void SpawnRequiredCard()
    {
        if(Deck.CurrentDeckCount > 0)
            SpawnCard(Deck.GetFirstCard());
    }

    public override void OnCardAdded()
    {
        if (!_canSpawnCards)
            return;

        base.OnCardAdded();
    }

    public void SetCanSpawnCards(bool value)
    {
        _canSpawnCards = value;
    }
}
