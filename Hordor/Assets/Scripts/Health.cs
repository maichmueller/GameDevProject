using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public AudioClip hurtSound;
    public AudioClip deathSound;
    private AudioSource audio;
    public float MAXHealth = 100;
    public bool alive = true;
    public float currentHealth;

    public event Action DeathEvent = delegate { };
    public event Action<float, float> HealthChangeEvent = delegate { };

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        currentHealth = MAXHealth;
        alive = true;
    }

    public void TakeDamage(float amount)
    {

        Debug.Log("Object " + this.gameObject.name + " took " + amount + " damage.");
        currentHealth = Math.Max(0, currentHealth - amount);
        RaiseHealthChangeEvent(MAXHealth, -amount);
        if (currentHealth <= 0)
        {
            audio.clip = deathSound;
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Kill();
        }
        else
        {
            //Debug.Log("Object " + this.gameObject.name + "Plays hurt sound");
            audio.clip = hurtSound;
            audio.Play();
        }
        Debug.Log("Remaining health " + currentHealth);
    }

    public void Kill()
    {
        alive = false;
        RaiseDeathEvent();
    }
    
    public void Heal(float amount)
    {
        currentHealth = Math.Min(currentHealth, currentHealth + amount);
        if (!alive && amount > 0)
        {
            alive = true;
        }
        RaiseHealthChangeEvent(MAXHealth, amount);
    }

    protected virtual void RaiseDeathEvent()
    {
        Debug.Log("Raising " + this.gameObject.name + "'s 'OnDeath' event.");
        // Raise the event in a thread-safe manner using the ?. operator.
        DeathEvent?.Invoke();
    }

    protected virtual void RaiseHealthChangeEvent(float max, float changeAbsolute)
    {
        //Debug.Log("Raising " + this.gameObject.name + "'s 'HealthChange' event.");
        // Raise the event in a thread-safe manner using the ?. operator.
        HealthChangeEvent?.Invoke(max, changeAbsolute);
    }

    // private void OnDestroy()
    // {
    //     audio.clip = deathSound;
    //     AudioSource.PlayClipAtPoint(deathSound, transform.position);
    // }
}