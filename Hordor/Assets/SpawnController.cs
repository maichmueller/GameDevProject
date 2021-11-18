using System;
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
    
    public event Action<GameObject> SpawnEvent = delegate { };
    
    void Start()
    {
        StartCoroutine(nameof(StartSpawn));
    }
    
    private IEnumerator StartSpawn()
    {
        while (true)
        {
            var chance = Mathf.Lerp(startChance, endChance, lerpProgress / 100);
            var roll = UnityEngine.Random.Range(0f, 1f);
            if (roll < chance / 100)
            {
                //Debug.Log("Success!, Rolled "+roll+"against chance of "+chance);
                var index = UnityEngine.Random.Range(0, enemies.Length);
                var gameObject = Instantiate(enemies[index], transform.position, transform.rotation);
                RaiseSpawnEvent(gameObject);
            }
            else
            {
                //Debug.Log("Failure!, Rolled "+roll+"against chance of "+chance);
            }
            yield return new WaitForSeconds(timeBetweenSpawn);
            lerpProgress += lerpIncrement;
        }
    }
    
    protected virtual void RaiseSpawnEvent(GameObject obj)
    {
        Debug.Log("Raising " + this.gameObject.name + "'s 'Spawn' event.");
        // Raise the event in a thread-safe manner using the ?. operator.
        SpawnEvent?.Invoke(obj);
    }
}
