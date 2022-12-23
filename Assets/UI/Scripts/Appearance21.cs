using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Appearance21 : MonoBehaviour
{
    [SerializeField] private ParticleSystem _glow;
    [SerializeField] private Image _image;
    [SerializeField] private Image _glowImage;
    [SerializeField] private Column _column;

    private Vector3 _startSize = new(0.8f, 0.8f, 0.8f);
    private Vector3 _targetSize = new(1f, 1f, 1f);

    private Color _targetColor = new(1, 1, 1, 1);

    private void OnEnable()
    {
        _column.MaxValueReachedChanged += OnMaxValueReachedChanged;
    }

    private void OnDisable()
    {
        _column.MaxValueReachedChanged -= OnMaxValueReachedChanged;
    }

    private void OnMaxValueReachedChanged(bool value)
    {
        if (!value)
            return;

        ShowImage();
        _glow.Play();
    }

    private void ShowImage()
    {
        _image.color = _targetColor;
        _image.transform.localScale = _startSize;

        _image.gameObject.SetActive(true);
        _glowImage.gameObject.SetActive(true);

        _image.transform.DOScale(_targetSize, 1f).OnComplete(DisableObject);
    }

    private void DisableObject() => _image.DOColor(_targetColor, 0.3f).OnComplete(EnableFalse);

    private void EnableFalse()
    {
        _image.gameObject.SetActive(false);
        _glowImage.gameObject.SetActive(false);
    }
}
