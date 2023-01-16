using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstJokerTutorialStage : TutorialStage
{
    [SerializeField] private Column[] _columns;
    [SerializeField] private Card[] _thirdColumnCards;
    [SerializeField] private Card[] _fourthColumnCards;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _spawnPosition1;
    [SerializeField] private Transform _spawnPosition2;
    [SerializeField] private Transform _spawnPosition3;
    [SerializeField] private Transform _spawnPosition4;

    public void AddCards()
    {
        Debug.Log("AddCards");


        var spawnedCard = Instantiate(_thirdColumnCards[0], _spawnPosition1.position, Quaternion.Euler(90, 180, 0), _spawnPosition);
        spawnedCard.Dragger.ChangeState(true);
        _columns[2].AddNewCard(spawnedCard);

        spawnedCard = Instantiate(_thirdColumnCards[1], _spawnPosition2.position, Quaternion.Euler(90, 180, 0), _spawnPosition);
        spawnedCard.Dragger.ChangeState(true);
        _columns[2].AddNewCard(spawnedCard);

        spawnedCard = Instantiate(_fourthColumnCards[0], _spawnPosition3.position, Quaternion.Euler(90, 180, 0), _spawnPosition);
        spawnedCard.Dragger.ChangeState(true);
        _columns[3].AddNewCard(spawnedCard);

        spawnedCard = Instantiate(_fourthColumnCards[1], _spawnPosition4.position, Quaternion.Euler(90, 180, 0), _spawnPosition);
        spawnedCard.Dragger.ChangeState(true);
        _columns[3].AddNewCard(spawnedCard);
    }
}
