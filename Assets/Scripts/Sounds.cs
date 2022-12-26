using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.WebUtility;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private AudioSource _chipSound;
    [SerializeField] private Level _level;
    [SerializeField] private AudioSource _loseSound;
    [SerializeField] private JokerGiver _jokerGiver;
    [SerializeField] private AudioSource _maxValueReachedSound;
    [SerializeField] private AudioSource _JokerGaveSound;
    [SerializeField] private CardSpawner _cardSpawner;
    [SerializeField] private AudioSource _dealCardsSound;
    [SerializeField] private AudioSource _moveCardSound;
    [SerializeField] private AudioSource _dropCardSound;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    private const string IS_SOUNDS_ON = nameof(IS_SOUNDS_ON);
    private bool _isSoundsOn
    {
        get { return PlayerPrefs.GetInt(IS_SOUNDS_ON, 1) == 1; }
        set { PlayerPrefs.SetInt(IS_SOUNDS_ON, value ? 1 : 0); }
    }

    private void Start()
    {
        Load();
    }

    private void OnEnable()
    {
        _healthBar.HeartRemoved += PlayChipSound;
        _level.LevelLost += PlayLoseSound;
        _cardSpawner.DealCardsStared += PlayDealCardsSound;
        _cardSpawner.DealCardsEnd += StopDealCardsSound;
        _cardSpawner.MoveCardStarted += PlayMoveCardSound;
        _cardSpawner.CardDroped += PlayDropCardSound;
        _jokerGiver.MaxValueReached += PlayMaxValueReachedSound;
        _jokerGiver.JokerGave += PlayJokerGaveSound;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeEvent;
    }

    private void OnDisable()
    {
        _healthBar.HeartRemoved -= PlayChipSound;
        _level.LevelLost -= PlayLoseSound;
        _cardSpawner.DealCardsStared -= PlayDealCardsSound;
        _cardSpawner.DealCardsEnd -= StopDealCardsSound;
        _cardSpawner.MoveCardStarted -= PlayMoveCardSound;
        _cardSpawner.CardDroped -= PlayDropCardSound;
        _jokerGiver.MaxValueReached -= PlayMaxValueReachedSound;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeEvent;
    }

    public void OnSwitchSoundsButtonClicked()
    {
        _isSoundsOn = !_isSoundsOn;
        SwitchSounds(_isSoundsOn);
        ChangeSprite();
    }

    public void Load()
    {
        SwitchSounds(_isSoundsOn);
        ChangeSprite();
    }

    public void SwitchSounds(bool value)
    {
        AudioListener.pause = !value;
        AudioListener.volume = value ? 1 : 0;
    }

    private void ChangeSprite()
    {
        _soundImage.sprite = _isSoundsOn ? _soundOnSprite : _soundOffSprite;
    }

    private void PlayChipSound()
    {
        _chipSound.Play();
    }

    private void PlayLoseSound()
    {
        _loseSound.Play();
    }

    private void PlayMaxValueReachedSound()
    {
        _maxValueReachedSound.Play();
    }

    private void PlayDealCardsSound()
    {
        _dealCardsSound.Play();
    }

    private void StopDealCardsSound()
    {
        _dealCardsSound.Stop();
    }

    private void PlayMoveCardSound()
    {
        _moveCardSound.Play();
    }

    private void PlayDropCardSound()
    {
        _dropCardSound.Play();
    }

    private void PlayJokerGaveSound()
    {
        _JokerGaveSound.Play();
    }

    private void OnInBackgroundChangeEvent(bool value)
    {
        if (value)
            SwitchSounds(false);
        else
            Load();
    }
}
