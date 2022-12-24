using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriesCounter : MonoBehaviour
{
    private const string TRIES = nameof(TRIES);
    public int Tries
    {
        get { return PlayerPrefs.GetInt(TRIES, 0); }
        private set { PlayerPrefs.SetInt(TRIES, value); }
    }

    private void Awake()
    {
        Tries++;
    }

    public void Reset()
    {
        Tries = 0;
    }
}
