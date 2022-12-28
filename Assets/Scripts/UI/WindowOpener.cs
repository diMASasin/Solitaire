using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class WindowOpener : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void OnValidate()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open()
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts= true;
    }

    public virtual void Close()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts= false;
    }
}
