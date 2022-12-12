using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class CardDrag : MonoBehaviour
{
  private Vector3 _offset;
  private Vector3 _startPosition;
  private Card _card;

  private float _mousePointZ;

  private bool _inColumn = false;

  private void Start()
  {
    _card = GetComponent<Card>();
    _startPosition = transform.position;
  }

  private void FixedUpdate()
  {
    Debug.DrawRay(transform.position, -transform.up);
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
    var ray = new Ray(transform.position, -transform.up);
    RaycastHit hit;

    if (!Physics.Raycast(ray, out hit)) return;
    if (!hit.collider.TryGetComponent(out Column column)) return;

    print("коснулся столба");
    ChangeState(true);
    column.AddNewCard(_card);
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
    else
      transform.DOMove(_startPosition, duration * Time.deltaTime).SetEase(Ease.Linear);
  }

  public void ChangeState(bool state)
  {
    _inColumn = state;
  }
}