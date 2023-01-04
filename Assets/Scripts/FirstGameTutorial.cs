using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGameTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private CardSpawner _cardSpawner;
    [SerializeField] private GameObject _table;

    private const string TUTORIAL_SHOWED = nameof(TUTORIAL_SHOWED);
    private bool _tutorialShowed
    {
        get { return PlayerPrefs.GetInt(TUTORIAL_SHOWED, 0) == 1; }
        set { PlayerPrefs.SetInt(TUTORIAL_SHOWED, value ? 1 : 0); }
    }

    private void Start()
    {
        if(!_tutorialShowed)
        {
            _tutorial.SetActive(true);
            _table.SetActive(true);
            _tutorialShowed = true;
        }
        else
        {
            _cardSpawner.StartDealCards();
        }
    }
}
