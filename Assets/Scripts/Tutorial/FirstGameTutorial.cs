using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstGameTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _blackout;
    [SerializeField] private CardSpawner _tutorialCardSpawner;
    [SerializeField] private CardSpawner _gameCardSpawner;
    [SerializeField] private GameObject _table;
    [SerializeField] private Column[] _columns;
    [SerializeField] private TutorialStage[] _tutorialStages;
    [SerializeField] private GameObject _hand;

    private int _tutorialStageIndex = 0;
    private bool _needToShowNextStage = false;

    private const string TUTORIAL_SHOWED = nameof(TUTORIAL_SHOWED);
    private bool _tutorialShowed
    {
        get { return PlayerPrefs.GetInt(TUTORIAL_SHOWED, 0) == 1; }
        set { PlayerPrefs.SetInt(TUTORIAL_SHOWED, value ? 1 : 0); }
    }

    private void OnEnable()
    {
        foreach (var column in _columns)
            column.CardAdded += OnCardAdded;

        foreach (var stage in _tutorialStages)
        {
            stage.StageShowed += OnStageShowed;
            stage.StageHid += OnStageHid;
        }

        _tutorialCardSpawner.CardDroped += OnCardDropped;
    }

    private void OnDisable()
    {
        foreach (var column in _columns)
            column.CardAdded -= OnCardAdded;

        foreach (var stage in _tutorialStages)
        {
            stage.StageShowed -= OnStageShowed;
            stage.StageHid -= OnStageHid;
        }

        _tutorialCardSpawner.CardDroped -= OnCardDropped;
    }

    private void Start()
    {
        _hand.SetActive(false);

        if(!_tutorialShowed)
            ShowTutorial();
        else
            StartGame();
    }

    private void ShowTutorial()
    {
        _tutorialCardSpawner.StartDealCards();

        _blackout.SetActive(true);
        _tutorial.SetActive(true);
        _tutorialShowed = true;

        _needToShowNextStage = true;
    }

    private void OnCardDropped()
    {
        if(_needToShowNextStage)
        {
            foreach (var column in _columns)
                column.enabled = false;

            _tutorialStages[_tutorialStageIndex].Show();
            _needToShowNextStage = false;
        }
    }

    private void OnStageShowed()
    {
        _hand.SetActive(true);
    }

    private void OnStageHid()
    {
        _hand.SetActive(false);
        _needToShowNextStage = true;
    }

    private void OnCardAdded()
    {
        _tutorialStages[_tutorialStageIndex].Hide();
        _tutorialStageIndex++;

        if(_tutorialStageIndex < _tutorialStages.Length)
            _tutorialStages[_tutorialStageIndex].Show();
        else
            OnTutorialCompleted();
    }

    private void OnTutorialCompleted()
    {
        _blackout.SetActive(false);
        _tutorial.SetActive(false);

        foreach (var column in _columns)
        {
            column.Reset();
            column.ResetMaxValueReached();
        }

        _tutorialCardSpawner.gameObject.SetActive(false);
        StartGame();
    }

    private void StartGame()
    {
        _gameCardSpawner.enabled = true;
        _gameCardSpawner.StartDealCards();
    }
}
