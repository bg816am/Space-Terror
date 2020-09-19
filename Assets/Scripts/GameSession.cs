using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score;
    private int health;

    private void Awake()
    {
        SetUpSingleton();
        
    }

    public int GetScore()
    {
        return score;
    }

    

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void SubtractHealth(int healthAmount)
    {
        health -= healthAmount;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void SetUpSingleton()
    {
        int gameSessions = FindObjectsOfType<GameSession>().Length;
        if (gameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
