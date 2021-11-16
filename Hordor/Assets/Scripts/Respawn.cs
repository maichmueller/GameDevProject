using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Respawn : MonoBehaviour
{
    public Vector3 spawnPoint;
    public bool respawn = false;
    private Health health;

    // Start is called before the first frame update
    void OnEnable()
    {
        this.gameObject.transform.position = spawnPoint;
        health = this.gameObject.GetComponent<Health>();

    }

    private void OnDisable()
    {
        if (respawn && !health.alive)
        {
            RespawnObject();
        }
    }
    

    void RespawnObject()
    {
        this.gameObject.transform.position = spawnPoint;
        health.Heal(health.MAXHealth);
        this.gameObject.SetActive(true);
    }
}