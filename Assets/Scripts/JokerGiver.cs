using UnityEngine;
using UnityEngine.UI;

public class JokerGiver : MonoBehaviour
{
    [SerializeField] private Column[] _columns;
    [SerializeField] private Button _jokerButton;
    [SerializeField] private GameObject _jokerTemplate;
    [SerializeField] private CardSpawner _cardSpawner;

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

        ResetMaxValueReached();

        var joker = Instantiate(_jokerTemplate, Vector3.zero, Quaternion.Euler(new Vector3(90, 0, 0))).GetComponent<Card>();

        _cardSpawner.InsertInFirst(joker);
        _cardSpawner.DestroyCard();
        _cardSpawner.ShowFirstCard();
        _cardSpawner.MoveSpawnedCardsBack();
    }

    private void OnMaxValueReachedChanged()
    {
        foreach (var column in _columns)
            if (!column.IsMaxValueReached)
                return;

        _jokerButton.gameObject.SetActive(true);
    }

    private void ResetMaxValueReached()
    {
        foreach (var column in _columns)
            column.ResetMaxValueReached();
    }
}
