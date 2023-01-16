using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCardSpawner : CardSpawner
{
    public override void SpawnRequiredCard()
    {
        if(Deck.CurrentDeckCount > 0)
            SpawnCard(Deck.GetFirstCard());
    }
}
