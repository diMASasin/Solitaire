using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondJokerTutorial : TutorialStage
{
    [SerializeField] private GameObject _window;
    [SerializeField] private GameObject _invertedWindow;

    public override void Show()
    {
        _invertedWindow.SetActive(true);
        _window.SetActive(false);
        base.Show();
    }
}
