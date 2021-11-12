using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public const float minVelocityForDamage = 5;

    private void OnCollisionEnter(Collision other)
    {
        float damageFromSpeed = other.relativeVelocity.magnitude * 2;
        if (damageFromSpeed >= minVelocityForDamage)
        {
            this.gameObject.GetComponent<Health>().TakeDamage(damageFromSpeed);
        }
    }
}