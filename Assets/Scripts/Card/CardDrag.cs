using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class CardDrag : MonoBehaviour
{
    [SerializeField] private Card _card;
    private Vector3 _offset;
    private Vector3 _startPosition;

    private float _mousePointZ;

    private bool _inColumn = false;
    private bool _onStartPosition = false;

    public bool CanDrag { get; private set; } = false;

    public event Action CardTook;
    public event Action CardDroped;

    private void OnValidate()
    {
        _card = GetComponent<Card>();
    }

    //private void FixedUpdate()
    //{
    //  Debug.DrawRay(transform.position, -transform.up);
    //}

    private void OnMouseDown()
    {
        _onStartPosition = transform.position == _startPosition;
        if (!_onStartPosition)
            return;

        _mousePointZ = Camera.main.WorldToScreenPoint(transform.position).z;
        _offset = transform.position - GetMouseWorldPosition();

        CardTook?.Invoke();
    }

    private void OnMouseDrag()
    {
        if (CanDrag && _onStartPosition)
            transform.position = GetMouseWorldPosition() + _offset;
    }

    private void OnMouseUp()
    {
        if (!CanDrag)
            return;

        var ray = new Ray(transform.position, -transform.up);
        bool raycast = Physics.Raycast(ray, out RaycastHit hit);
        Column column = null;
        if (raycast)
            hit.collider.TryGetComponent(out column);

        if (!raycast || !column.enabled)
        {
            float duration = 10f;
            Tween tween = transform.DOMove(_startPosition, duration * Time.deltaTime).SetEase(Ease.Linear);
            tween.OnComplete(() =>
            {
                transform.position = _startPosition;
                CardDroped?.Invoke();
            });
            return;
        }

        if (!column)
            return;

        ChangeState(true);
        column.AddNewCard(_card);
        CardDroped?.Invoke();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = _mousePointZ;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void MoveToPoint(Vector3 point)
    {
        var duration = 10f;

        if (_inColumn)
            transform.DOMove(point, duration * Time.deltaTime).SetEase(Ease.Linear);
    }

    private void ChangeState(bool state)
    {
        _inColumn = state;
    }

    public void SetCanDrag(bool value)
    {
        CanDrag = value;

        if (value)
            _startPosition = transform.position;
    }
}