using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip[] clips;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Reached");
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            audio.clip = clips[i];
            audio.Play();
            if (i < 5)i++;
        }
    }
}
