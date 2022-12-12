using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private List<Heart> _hearts;

    private int _index;

    private void Start() => _index = 0;

    public void RemoveHeart()
    {
        _hearts[_index].gameObject.SetActive(false);

        if (_index == 2)
            Time.timeScale = 0;

        _index++;
    }
}
