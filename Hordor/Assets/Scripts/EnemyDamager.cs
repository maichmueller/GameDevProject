using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public const float minVelocityForDamage = 50;
    public ParticleSystem damagePS = null;
    private float psTime = 5f;

    private void OnEnable()
    {
        this.gameObject.GetComponent<Health>().HealthChangeEvent += OnHealthChange;
    }

    private void OnCollisionEnter(Collision other)
    {
        float damageFromSpeed = other.relativeVelocity.magnitude * 2;
        if (damageFromSpeed >= minVelocityForDamage)
        {
            this.gameObject.GetComponent<Health>().TakeDamage(damageFromSpeed);
        }
    }

    private void OnHealthChange(float _, float __)
    {
        if (damagePS)
        {
            ParticleSystem damagePSInstance = Instantiate(damagePS,
                this.gameObject.transform.position, Quaternion.identity);
            damagePSInstance.Play();
            Destroy(damagePSInstance, psTime);
        }
    }
}