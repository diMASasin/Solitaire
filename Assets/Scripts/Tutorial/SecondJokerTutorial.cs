using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondJokerTutorial : TutorialStage
{
    [SerializeField] private GameObject _window;
    [SerializeField] private GameObject _invertedWindow;
    [SerializeField] private Column[] _columns;
    [SerializeField] private Card[] _thirdColumnCards;
    [SerializeField] private Card[] _fourthColumnCards;
    [SerializeField] private Transform _spawnPosition;

    public void AddCards()
    {
        foreach (var card in _thirdColumnCards)
        {
            var spawnedCard = Instantiate(card, _spawnPosition.position, Quaternion.Euler(90, 180, 0), _spawnPosition);
            spawnedCard.Dragger.ChangeState(true);
            spawnedCard.transform.position = _columns[2].transform.position;
            _columns[2].AddNewCard(spawnedCard);
        }

        foreach (var card in _fourthColumnCards)
        {
            var spawnedCard = Instantiate(card, _spawnPosition.position, Quaternion.Euler(90, 180, 0), _spawnPosition);
            spawnedCard.Dragger.ChangeState(true);
            spawnedCard.transform.position = _columns[3].transform.position;
            _columns[3].AddNewCard(spawnedCard);
        }
    }

    public override void Show()
    {
        _invertedWindow.SetActive(true);
        _window.SetActive(false);
        base.Show();
    }
}
