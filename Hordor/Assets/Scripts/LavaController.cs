using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    private GameObject player;

    private Coroutine _startedCoroutine;

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
        else if(otherGO.CompareTag("Player"))
        {
            _startedCoroutine = StartCoroutine(Damage());
        }
    }

    private void OnCollisionStay(Collision other)
    {
        player.GetComponent<Health>().TakeDamage(playerDamagePerSecond * Time.deltaTime);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(_startedCoroutine);
        }
    }

    IEnumerator Damage()
    {
        while (true)
        {
            player.GetComponent<Health>().TakeDamage(playerDamagePerSecond * Time.deltaTime);
            yield return null;
        }
    }
}