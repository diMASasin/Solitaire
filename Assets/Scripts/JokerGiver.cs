using System;
using UnityEngine;
using UnityEngine.UI;

public class JokerGiver : MonoBehaviour
{
    [SerializeField] private Column[] _columns;
    [SerializeField] private Button _jokerButton;
    [SerializeField] private GameObject _jokerTemplate;
    [SerializeField] private CardSpawner _cardSpawner;
    [SerializeField] private ParticleSystem[] _columnsEffect;
    [SerializeField] private ParticleSystem _jokerEffect;

    public event Action JokerButtonEnabled;
    public event Action JokerAdButtonClicked;
    public event Action MaxValueReached;

    private void OnValidate()
    {
        _columns = FindObjectsOfType<Column>();
    }

    private void OnEnable()
    {
        foreach (var column in _columns)
            column.MaxValueReachedChanged += OnMaxValueReachedChanged;
    }

    private void OnDisable()
    {
        foreach (var column in _columns)
            column.MaxValueReachedChanged -= OnMaxValueReachedChanged;
    }


    public void GiveJoker()
    {
        if (_cardSpawner.ShowingCard == null || !_cardSpawner.ShowingCard.Dragger.CanDrag)
            return;

        JokerAdButtonClicked?.Invoke();
        ResetMaxValueReached();

        var joker = Instantiate(_jokerTemplate, Vector3.zero, Quaternion.Euler(new Vector3(90, 0, 0))).GetComponent<Card>();

        _cardSpawner.InsertInFirst(joker);
        _cardSpawner.DestroyCard();
        _cardSpawner.ShowFirstCard();
        _cardSpawner.MoveSpawnedCardsBack();
    }

    private void OnMaxValueReachedChanged(bool value)
    {
        if (!value)
            return;

        foreach (var column in _columns)
        {
            if (!column.IsMaxValueReached)
            {
                MaxValueReached?.Invoke();
                return;
            }
        }

        if (_jokerButton.gameObject.activeSelf)
        {
            MaxValueReached?.Invoke();
            return;
        }

        JokerButtonEnabled?.Invoke();
        _jokerButton.gameObject.SetActive(true);

        _jokerEffect.Play();

        foreach (var effect in _columnsEffect)
            effect.Play();
    }

    private void ResetMaxValueReached()
    {
        foreach (var column in _columns)
            column.ResetMaxValueReached();
    }
}
