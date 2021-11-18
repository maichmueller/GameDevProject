using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    public float pushBackFactor;
    public ParticleSystem damagePS = null;
    public float damageFactor = 1f;
    private float damageImmune = 1;
    private float elapsed;

    private Health healthComp;
    private float psTime = 5f;

    private void Start()
    {
        healthComp = this.gameObject.GetComponent<Health>();
        elapsed = damageImmune;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
    }

    private void OnEnable()
    {
        this.gameObject.GetComponent<Health>().HealthChangeEvent += OnHealthChange;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (this.gameObject.GetComponent<Health>().alive)
        {
            if (other.gameObject.CompareTag("Enemy") && elapsed >= damageImmune)
            {
                elapsed = 0f;
                var lastVelocity = other.gameObject.GetComponent<EnemyStateMachine>().lastVelocity;
                Debug.Log(lastVelocity);
                var damageFromSpeed = lastVelocity.magnitude / 4;
                Debug.Log(damageFromSpeed);
                healthComp.TakeDamage(damageFromSpeed * damageFactor);
                this.gameObject.GetComponent<Rigidbody>().AddForce(lastVelocity * pushBackFactor, ForceMode.Impulse);
            }
            else if (other.gameObject.CompareTag("Bullet"))
            {
                healthComp.TakeDamage(other.gameObject.GetComponent<BulletController>().damage);
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