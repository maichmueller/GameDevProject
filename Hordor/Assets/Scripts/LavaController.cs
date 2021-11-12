using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var otherGO = other.gameObject;
        if (otherGO.CompareTag("Player") || otherGO.CompareTag("Enemy"))
        {
            Debug.Log("Killable object hit lava");
            otherGO.GetComponent<Health>().Kill();
        }
    }
}