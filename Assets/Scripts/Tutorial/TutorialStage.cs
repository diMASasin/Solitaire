using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

enum HandAnimations
{
    None,
    Column1,
    Column2,
    Column3,
    Column4,
    Joker
}

public class TutorialStage : MonoBehaviour
{
    [SerializeField] private Column _activeColumn;
    [SerializeField] private int _requiredOrderInLayer = 1;
    [SerializeField] private GameObject _tutorialText;
    [SerializeField] private Animator _handAnimator;
    [SerializeField] private HandAnimations _handAnimations;

    private Canvas _columnCanvas;
    private int _defaultOrderInLayer;

    public event Action StageShowed;
    public event Action StageHid;

    private void Awake()
    {
        _columnCanvas = _activeColumn.GetComponentInChildren<Canvas>();
        _defaultOrderInLayer = _columnCanvas.sortingOrder;
    }

    public virtual void Show()
    {
        StageShowed?.Invoke();
        _handAnimator.SetTrigger(_handAnimations.ToString());
        _activeColumn.enabled = true;
        _tutorialText.gameObject.SetActive(true);
        _columnCanvas.sortingOrder = _requiredOrderInLayer;
    }

    public virtual void Hide()
    {
        _handAnimator.SetTrigger(HandAnimations.None.ToString());
        _tutorialText.gameObject.SetActive(false);
        _columnCanvas.sortingOrder = _defaultOrderInLayer;
        StageHid?.Invoke();
    }
}
