using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using EZCameraShake;
using Unity.Mathematics;
using UnityEngine.ProBuilder;

public class ConeViewTrigger : MonoBehaviour
{
    public HashSet<GameObject> inConeObjects = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            inConeObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inConeObjects.Remove(other.gameObject);
    }
}