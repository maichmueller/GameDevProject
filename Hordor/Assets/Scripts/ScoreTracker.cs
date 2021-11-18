using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Health health;
    public float scoreIncrease = 10;
    public Text scoreText;

    private void Start()
    {
        health.DeathEvent+=UpdateScore;
    }

    void UpdateScore()
    {
        Debug.Log("REACHED");
        var current = float.Parse(scoreText.text);
        scoreText.text = (current + scoreIncrease).ToString();
    }
        
}
