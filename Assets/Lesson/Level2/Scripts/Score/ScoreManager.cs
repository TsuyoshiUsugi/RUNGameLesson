using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonobehavior<ScoreManager>
{
    public event Action<int> AddPointEvent;

    int _score = 0;
    int _addpoint = 1;

    public void AddScore()
    {
        _score += _addpoint;
        if (AddPointEvent != null) AddPointEvent(_score);
    }
}
