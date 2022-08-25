using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public Events.StringEvent onScoreChange;

    private int scoreVal = 0;

    public int ScoreValue
    {
        get { return scoreVal; }
    }

    private void Score()
    {
        scoreVal++;
        
        onScoreChange.Invoke(scoreVal.ToString());
    }

    private void ResetScore()
    {
        scoreVal = 0;
        
        onScoreChange.Invoke(scoreVal.ToString());
    }

    private void OnEnable()
    {
        Snake.Singleton.onSnakeEates += Score;
        Snake.Singleton.onSnakeDies += ResetScore;
    }
    
    private void OnDisable()
    {
        Snake.Singleton.onSnakeEates -= Score;
        Snake.Singleton.onSnakeDies -= ResetScore;

    }
}
