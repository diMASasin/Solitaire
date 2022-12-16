using MPUIKIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnView : MonoBehaviour
{
    [SerializeField] private Column _column;
    [SerializeField] private MPImage _image;

    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = _image.OutlineColor;
    }

    private void OnEnable()
    {
        _column.MaxValueReachedChanged += OnMaxValueReached;
    }

    private void OnDisable()
    {
        _column.MaxValueReachedChanged -= OnMaxValueReached;
    }

    private void OnMaxValueReached()
    {
        if(_column.IsMaxValueReached)
            _image.OutlineColor = Color.yellow;
        else
            _image.OutlineColor = _defaultColor;
    }
}
