using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MAXHealth = 100;
    public bool alive = true;
    private float _currentHealth;

    public event Action DeathEvent = delegate { };
    public event Action<float, float> HealthChangeEvent = delegate { };

    private void OnEnable()
    {
        _currentHealth = MAXHealth;
        alive = true;
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("Object " + this.gameObject.name + " took " + amount + " damage.");
        _currentHealth -= amount;
        RaiseHealthChangeEvent(MAXHealth, amount);
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        alive = false;
        RaiseDeathEvent();
    }

    protected virtual void RaiseDeathEvent()
    {
        Debug.Log("Raising " + this.gameObject.name + "'s 'OnDeath' event.");
        // Raise the event in a thread-safe manner using the ?. operator.
        DeathEvent?.Invoke();
    }

    protected virtual void RaiseHealthChangeEvent(float max, float changeAbsolute)
    {
        Debug.Log("Raising " + this.gameObject.name + "'s 'HealthChange' event.");
        // Raise the event in a thread-safe manner using the ?. operator.
        HealthChangeEvent?.Invoke(max, changeAbsolute);
    }
}