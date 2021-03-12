using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI score;
    private int scoreValue = 0;
    private int healthValue = 3;
    private int ballsCount = 1;

    private void Update()
    {
        if (scoreValue >= 750)
        {
            SceneManager.LoadScene("You Win");
        }
    }

    public int ScoreValue
    {
        get => scoreValue;
        set => scoreValue = value;
    }

    public int HealthValue
    {
        get => healthValue;
        set => healthValue = value;
    }

    public int BallsCount
    {
        get => ballsCount;
        set => ballsCount = value;
    }
}
