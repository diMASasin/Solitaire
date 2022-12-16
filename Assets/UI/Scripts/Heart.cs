using DG.Tweening;
using UnityEngine;

public class Heart : MonoBehaviour
{
  private Rigidbody _rigidbody;
  private Vector3 _startRotation = new Vector3(90, 0, 0);
  private Tween _tween;

  private void Awake()
  {
    _rigidbody = GetComponentInChildren<Rigidbody>();
    _startRotation = _rigidbody.rotation.eulerAngles;
  }

  public void ReturnDefaultState()
  {
    _tween.Kill();
    _rigidbody.isKinematic = true;
    _rigidbody.gameObject.transform.rotation = Quaternion.Euler(_startRotation);
    _rigidbody.gameObject.transform.localPosition = Vector3.zero;
  }

  public void MoveToStartPosition()
  {
    const float rotationDuration = .2f;
    const int force = 150;
    var rotation = new Vector3(360, 0, 0);

    _rigidbody.isKinematic = false;
    _rigidbody.AddForce((Vector3.up + Vector3.left) * force);

    _tween = _rigidbody
      .DORotate(rotation, rotationDuration)
      .SetEase(Ease.Flash)
      .SetLoops(-1, LoopType.Incremental);
  }
}