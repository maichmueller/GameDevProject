using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    public float pushBackFactor;
    public ParticleSystem damagePS = null;
    public float damageFactor = 1f;

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
            var lastVelocity = other.gameObject.GetComponent<EnemyStateMachine>().lastVelocity;
            var damageFromSpeed = lastVelocity.magnitude;
            healthComp.TakeDamage(damageFromSpeed * damageFactor);
            this.gameObject.GetComponent<Rigidbody>().AddForce(lastVelocity * pushBackFactor, ForceMode.Impulse);
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