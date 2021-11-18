using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemies;
    public float startChance;
    public float endChance;
    public float lerpIncrement;
    public float timeBetweenSpawn;
    private float lerpProgress;
    void Start()
    {
        StartCoroutine(nameof(StartSpawn));
    }
    
    private IEnumerator StartSpawn()
    {
        while (true)
        {
            var chance = Mathf.Lerp(startChance, endChance, lerpProgress / 100);
            var roll = Random.Range(0f, 1f);
            if (roll < chance / 100)
            {
                //Debug.Log("Success!, Rolled "+roll+"against chance of "+chance);
                var index = Random.Range(0, enemies.Length);
                Instantiate(enemies[index], transform.position, transform.rotation);
            }
            else
            {
                //Debug.Log("Failure!, Rolled "+roll+"against chance of "+chance);
            }
            yield return new WaitForSeconds(timeBetweenSpawn);
            lerpProgress += lerpIncrement;
        }
    }
}
