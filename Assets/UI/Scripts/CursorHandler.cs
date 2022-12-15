using UnityEngine;
using DG.Tweening;

public class CursorHandler : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            _cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7.61f)); 
    }

    //[SerializeField] private SpriteRenderer _cursorSpriteRenderer;
    //[SerializeField] private List<Transform> _movePoints;

    //private Color _targetColor = new(1, 1, 1, 1);
    //private Color _startColor = new(1, 1, 1, 0);

    //private int _firstPointIndex = 1;
    //private int _zeroPointIndex = 0;

    //private void Start() => _cursorSpriteRenderer.color = _startColor;

    //public void MoveToPoint()
    //{
    //    _cursorSpriteRenderer.DOColor(_targetColor, 0.5f);
    //    _cursorSpriteRenderer.gameObject.transform.DOMoveY(_movePoints[_firstPointIndex].transform.position.y, 0.5f).SetDelay(0.5f);
    //    _cursorSpriteRenderer.DOColor(_startColor, 0.5f).SetDelay(1f).OnComplete(RestartPosition); 
    //}

    //private void RestartPosition() => _cursorSpriteRenderer.transform.position = _movePoints[_zeroPointIndex].position;
}
