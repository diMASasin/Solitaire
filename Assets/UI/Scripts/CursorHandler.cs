using UnityEngine;
using DG.Tweening;
public class CursorHandler : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;

    private Vector3 _startScale = new (1, 1, 1);
    private Vector3 _targetScale = new (0.8f, 0.8f, 0.8f);

    private void Update()
    {

        _cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7.61f));

        if (Input.GetMouseButton(0))
            _cursor.transform.DOScale(_targetScale, 0.2f);

        else
            _cursor.transform.DOScale(_startScale, 0.2f);

    }
}
