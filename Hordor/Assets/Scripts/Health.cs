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
    private float _currentHealth;
    private Text scoreText;

    public event Action DeathEvent = delegate { };
    public event Action<float, float> HealthChangeEvent = delegate { };

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
    }

    private void OnEnable()
    {
        _currentHealth = MAXHealth;
        alive = true;
    }

    public void TakeDamage(float amount)
    {   
        //
        if (gameObject.CompareTag("Enemy"))
        {
            var deathMod = 1;
            if (_currentHealth <= 0) deathMod += 2;
            var current = float.Parse(scoreText.text);
            var score = Mathf.Round(current + ((1 * amount) * deathMod));
            scoreText.text = score.ToString();
        }
        
        
        Debug.Log("Object " + this.gameObject.name + " took " + amount + " damage.");
        _currentHealth = Math.Max(0, _currentHealth - amount);
        RaiseHealthChangeEvent(MAXHealth, -amount);
        if (_currentHealth <= 0)
        {
            Kill();
        }
        else
        {
            //Debug.Log("Object " + this.gameObject.name + "Plays hurt sound");
            audio.clip = hurtSound;
            audio.Play();
        }
    }

    public void Kill()
    {
        alive = false;
        RaiseDeathEvent();
    }
    
    public void Heal(float amount)
    {
        _currentHealth = Math.Min(_currentHealth, _currentHealth + amount);
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

    private void OnDestroy()
    {
        audio.clip = deathSound;
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }
}