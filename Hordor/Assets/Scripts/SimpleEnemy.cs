using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    public GameObject player;
    private bool _phyisicsEnabled;

    private Vector3 lastpos;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();
        _phyisicsEnabled = false;
        lastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_phyisicsEnabled)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (_phyisicsEnabled && lastpos == transform.position)
        {
            Debug.Log("REACHED");
            rb.isKinematic = true;
            agent.enabled = true;
            _phyisicsEnabled = false;
        }
        lastpos = transform.position;
    }

    public void EnablePhysics()
    {
        agent.isStopped = true;
        rb.isKinematic = false;
        agent.enabled = false;
        _phyisicsEnabled = true;
    }
}
