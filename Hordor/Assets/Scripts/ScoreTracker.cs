using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public List<SpawnController> SpawnControllers;
    public float scoreIncrease = 10;
    public Text scoreText;

    private void Start()
    {
        foreach (SpawnController spc in SpawnControllers)
        {
            spc.SpawnEvent += OnSpawn;
        }
    }

    void OnSpawn(GameObject obj)
    {
        if (obj.CompareTag("Enemy"))
        {
            obj.GetComponent<Health>().DeathEvent += UpdateScore;
        }
    }

    void UpdateScore()
    {
        Debug.Log("REACHED");
        var current = float.Parse(scoreText.text);
        scoreText.text = (current + scoreIncrease).ToString();
    }
        
}
