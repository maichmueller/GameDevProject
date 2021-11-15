using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyStateMachine : MonoBehaviour
{
    // Start is called before the first frame update

    public State defaultState;
    private State _physicsState;
    protected State _state;
    public bool physicsEnabled;
    public NavMeshAgent agent;
    public Rigidbody rb;
    private Vector3 _lastpos;

    // Update is called once per frame

    public void ChangeState(State state)
    {
        _physicsState = gameObject.GetComponent<PhysicsState>();
        _state = state;
        _state.Activate();
    }

    protected virtual void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();
        physicsEnabled = false;
        _lastpos = transform.position;
    }
    
    protected virtual void Update()
    {
        Debug.Log(_state.ToString());
        _state.Behaviour();
    }
    
    protected virtual void FixedUpdate()
    {
        if (physicsEnabled && _lastpos == transform.position)
        {
            rb.isKinematic = true;
            agent.enabled = true;
            physicsEnabled = false;
        }
        _lastpos = transform.position;
    }

    public void EnablePhysics()
    {
        ChangeState(_physicsState);
        agent.isStopped = true;
        rb.isKinematic = false;
        agent.enabled = false;
        physicsEnabled = true;
    }
}
