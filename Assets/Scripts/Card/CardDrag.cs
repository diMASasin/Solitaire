using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class CardDrag : MonoBehaviour
{
  private Vector3 _offset;
  private Vector3 _targetPosition;
  private Vector3 _startPosition;

  private float _mousePointZ;

  private bool _inColumn = false;

  private void Start()
  {
    _startPosition = transform.position;
  }

  private void OnMouseDown()
  {
    _mousePointZ = Camera.main.WorldToScreenPoint(transform.position).z;

    _offset = transform.position - GetMouseWorldPosition();
  }

  private void OnMouseDrag()
  {
    transform.position = GetMouseWorldPosition() + _offset;
  }

  private void OnMouseUp()
  {
    float duration = 10f;

    transform.DOMove(_targetPosition, duration * Time.deltaTime).SetEase(Ease.Linear);
  }

  private Vector3 GetMouseWorldPosition()
  {
    Vector3 mousePoint = Input.mousePosition;

    mousePoint.z = _mousePointZ;

    return Camera.main.ScreenToWorldPoint(mousePoint);
  }

  public void MoveToPoint(Vector3 point)
  {
    float duration = 10f;

    if (_inColumn)
      transform.DOMove(point, duration * Time.deltaTime).SetEase(Ease.Linear);
    else
      transform.DOMove(_targetPosition, duration * Time.deltaTime).SetEase(Ease.Linear);
  }

  public void ChangeState(bool state)
  {
    _inColumn = state;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent(out Column card))
    {
      _targetPosition = card.transform.position;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.TryGetComponent(out Column card))
    {
      _targetPosition = _startPosition;
    }
  }
}