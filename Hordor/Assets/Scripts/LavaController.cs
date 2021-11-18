using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public GameObject player;

    public float playerDamagePerSecond = 10f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
        var otherGO = other.gameObject;
        if (otherGO.CompareTag("Enemy"))
        {
            Debug.Log("Killable object hit lava");
            otherGO.GetComponent<Health>().Kill();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Damage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        while (true)
        {
            player.GetComponent<Health>().TakeDamage(playerDamagePerSecond);
            yield return null;
        }
    }
}