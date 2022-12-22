using MPUIKIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColumnView : MonoBehaviour
{
    [SerializeField] private Column _column;
    [SerializeField] private Image _image;
    [SerializeField] private Color _maxValueReachedColor;

    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = _image.color;
    }

    private void OnEnable()
    {
        _column.MaxValueReachedChanged += OnMaxValueReached;
    }

    private void OnDisable()
    {
        _column.MaxValueReachedChanged -= OnMaxValueReached;
    }

    private void OnMaxValueReached(bool value)
    {
        if(value)
            _image.color = _maxValueReachedColor;
        else
            _image.color = _defaultColor;
    }
}
