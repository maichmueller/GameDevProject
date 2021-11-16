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
    private bool _physicsEnabled;

    public Vector3 lastPos;
    public Vector3 lastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();
        _physicsEnabled = false;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_physicsEnabled)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (_physicsEnabled && lastPos == transform.position)
        {
            rb.isKinematic = true;
            agent.enabled = true;
            _physicsEnabled = false;
        }
        lastVelocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
    }

    public void EnablePhysics()
    {
        agent.isStopped = true;
        rb.isKinematic = false;
        agent.enabled = false;
        _physicsEnabled = true;
    }

    // private void OnCollisionStay(Collision other)
    // {
    //     other.gameObject.GetComponent<SimpleEnemy>().EnablePhysics();
    //     other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10000);
    // }
}