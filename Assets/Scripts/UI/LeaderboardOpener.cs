using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardOpener : WindowOpener
{
    [SerializeField] GameObject _table;

    public override void Open()
    {
        base.Open();
        _table.SetActive(true);
    }

    public override void Close()
    {
        base.Close();
        _table.SetActive(false);
    }
}
