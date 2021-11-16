using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public float playerDamagePerSecond = 10f;
    private void OnCollisionEnter(Collision other)
    {
        var otherGO = other.gameObject;
        if (otherGO.CompareTag("Enemy"))
        {
            Debug.Log("Killable object hit lava");
            otherGO.GetComponent<Health>().Kill();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        var otherGO = other.gameObject;
        if (otherGO.CompareTag("Player"))
        {
            otherGO.GetComponent<Health>().TakeDamage(playerDamagePerSecond);
        }
    }
}