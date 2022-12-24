using DG.Tweening;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private Vector3 _startRotation = new Vector3(90, 0, 0);
    private Tween _tween;

    private void OnValidate()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Awake()
    {
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
        float rotationDuration = Random.Range(0.05f, 0.4f);
        int force = Random.Range(100, 201);
        var rotation = new Vector3(360, Random.Range(-10, 11), 0);

        _rigidbody.isKinematic = false;

        Vector3 diraction = Random.insideUnitCircle;
        diraction.y = Mathf.Abs(diraction.y);
        diraction.z -= 0.5f;
        _rigidbody.AddForce(diraction * force);

        _tween = _rigidbody
          .DORotate(rotation, rotationDuration)
          .SetEase(Ease.Flash)
          .SetLoops(-1, LoopType.Incremental);
    }
}