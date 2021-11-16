using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour
{
    public ParticleSystem cubeParticleSystem;
    private float psTime = 10;

    private void Start()
    {
        cubeParticleSystem.GetComponent<Renderer>().material = this.gameObject.GetComponent<Renderer>().material;
        this.gameObject.GetComponent<Health>().DeathEvent += OnDeath;
    }

    void OnDeath()
    {
            Debug.Log("Calling " + this.gameObject.name+ "'s 'OnDeath' method.");
            Explode();
    }

    void Explode()
    {
        ParticleSystem cubeFirework = Instantiate(cubeParticleSystem,
            this.gameObject.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
        cubeFirework.Play();
        Destroy(cubeFirework, psTime);
    }
}