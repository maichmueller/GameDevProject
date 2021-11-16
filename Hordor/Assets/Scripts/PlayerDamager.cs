using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    public float pushBackFactor;
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            float damageFromSpeed = other.gameObject.GetComponent<SimpleEnemy>().lastVelocity.magnitude;
            healthComp.TakeDamage(damageFromSpeed);
            this.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.GetComponent<SimpleEnemy>().lastVelocity * pushBackFactor, ForceMode.Impulse);
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