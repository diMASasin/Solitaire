using UnityEngine;

[RequireComponent(typeof(CardDrag))]
public class Card : MonoBehaviour
{
    [SerializeField] private CardValues _valueName;
    [SerializeField] private CardSuits _suit;
    [SerializeField] private int _value;

    private CardDrag _dragger;

    public CardValues ValueName => _valueName;
    public CardSuits Suit => _suit;
    public virtual int Value => _value;
    public CardDrag Dragger => _dragger;

    private void OnValidate()
    {
        if ((int)_valueName <= 11)
            _value = (int)_valueName;
        else
            _value = 10;
    }

    private void Awake() => _dragger = GetComponent<CardDrag>();

    public void Move(Vector3 position) => _dragger.MoveToPoint(position);
}