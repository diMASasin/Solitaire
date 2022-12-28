using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]
public class UITimeHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _circle;

    private  int _minutesWaiting = 5;

    private WaitForSecondsRealtime waitingOneSecond = new(1);

    private const float Step = 0.2f;
    private const int OneSecond = 1;

    private void Start()
    {
        _circle.fillAmount = 1;
    }

    private void OnValidate()
    {
        _circle = GetComponent<Image>();
    }

    private IEnumerator ChangeTime(Action action)
    {
        while (_minutesWaiting > 0)
        {
            yield return waitingOneSecond;

            _minutesWaiting -= OneSecond;

            _text.text = _minutesWaiting.ToString();

            _circle.fillAmount -= Step;
        }
        
        action.Invoke();
    }

    public void StartTimer(Action action) => StartCoroutine(ChangeTime(action));
}
