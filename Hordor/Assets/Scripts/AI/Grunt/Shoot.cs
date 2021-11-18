using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shoot : State
{
    public AudioClip shootingSound;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public Transform player;
    public Reposition reposition;
    private float time;
    public float shootDelay;
    private AudioSource audio;

    public override void Activate()
    {
        player = GameObject.FindWithTag("Player").transform;
        audio = gameObject.GetComponent<AudioSource>();
        reposition = gameObject.GetComponent<Reposition>();
        time = 0f;
    }

    public override void Behaviour()
    {
        gameObject.transform.LookAt(player);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position * 1000, Color.green);
                if (time > shootDelay)
                {
                    Instantiate(bullet, bulletSpawn.transform.position ,bulletSpawn.transform.rotation);
                    audio.clip = shootingSound;
                    audio.Play();
                    time = 0f;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position * 1000, Color.yellow);
                _machine.ChangeState(reposition);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position * 1000, Color.red);
            _machine.ChangeState(reposition);
        }
    }

    public override void FixedBehaviour()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
    }
}
