using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;

    private void Update()
    {
        //if (Input.GetMouseButton(0))
            _cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7.61f)); 
    }
}
