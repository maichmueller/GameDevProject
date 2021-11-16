using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shoot : State
{
    public GameObject bullet;
    public GameObject bulletSpawn;
    public Transform player;
    public Reposition reposition;
    private float time;
    public float shootDelay;

    public override void Activate()
    {
        reposition = gameObject.GetComponent<Reposition>();
        time = 0f;
    }

    public override void Behaviour()
    {
        gameObject.transform.LookAt(player);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (time > shootDelay)
                {
                    Instantiate(bullet, bulletSpawn.transform.position ,bulletSpawn.transform.rotation);
                    time = 0f;
                }
            }
            else
            {
                _machine.ChangeState(reposition);
            }
        }
        else
        {
            _machine.ChangeState(reposition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
    }
}
