using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float minVelocityForDamage = 10;
    public ParticleSystem damagePS = null;

    private Health healthComp;
    private float psTime = 5f;

    private void Start()
    {
        healthComp = this.gameObject.GetComponent<Health>();
    }

    private void OnEnable()
    {
        this.gameObject.GetComponent<Health>().HealthChangeEvent += OnHealthChange;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision enter of " + this.gameObject.name);
        Debug.Log("Rel speed damage: " + other.relativeVelocity.magnitude * 4);
        if (other.gameObject.CompareTag("Bullet"))
        {
            healthComp.TakeDamage(other.gameObject.GetComponent<BulletController>().damage);
        }
        else
        {
            var mod = 2;
            if (other.gameObject.CompareTag("Player")) mod = 1;
            float damageFromSpeed = other.relativeVelocity.magnitude * mod;
                                    //* 4;
            if (damageFromSpeed >= minVelocityForDamage)
            {
                healthComp.TakeDamage(damageFromSpeed);
            }
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