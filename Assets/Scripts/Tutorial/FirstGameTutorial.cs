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
    [SerializeField] private JokerGiver _jokerGiver;
    [SerializeField] private Score _score;

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
        _jokerGiver.JokerAdButtonClicked += OnCardAdded;
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
        _jokerGiver.JokerAdButtonClicked -= OnCardAdded;
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
        _blackout.SetActive(true);
        _tutorial.SetActive(true);
        _tutorialShowed = true;

        _tutorialCardSpawner.StartDealCards();

        _needToShowNextStage = true;
    }

    private void OnCardDropped()
    {
        if(_needToShowNextStage)
        {
            SetColumnsEnabled(false);

            _needToShowNextStage = false;
            _tutorialStages[_tutorialStageIndex].Show();
        }
    }

    private void SetColumnsEnabled(bool value)
    {
        foreach (var column in _columns)
            column.enabled = value;
    }

    private void OnStageShowed()
    {
        _hand.SetActive(true);
    }

    private void OnStageHid()
    {
        _needToShowNextStage = true;
        _hand.SetActive(false);
    }

    private void OnCardAdded()
    {
        _tutorialStages[_tutorialStageIndex].Hide();
        _tutorialStageIndex++;

        if(_tutorialStageIndex < _tutorialStages.Length)
        {
            var _currentStage = _tutorialStages[_tutorialStageIndex];
            if (_currentStage is SecondJokerTutorial)
            {
                foreach (var column in _columns)
                    column.CardAdded -= OnCardAdded;
                (_currentStage as SecondJokerTutorial).AddCards();
                foreach (var column in _columns)
                    column.CardAdded += OnCardAdded;
            }

            _currentStage.Show();
        }
        else
        {
            OnTutorialCompleted();
        }
    }

    private void OnJokerButtonClicked()
    {
        _tutorialStages[_tutorialStageIndex].Hide();
        _tutorialStageIndex++;
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
        _score.Reset();
        SetColumnsEnabled(true);
        enabled = false;
    }
}
