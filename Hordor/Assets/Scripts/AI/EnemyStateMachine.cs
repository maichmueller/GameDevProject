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
    public Vector3 lastPos;
    public Vector3 lastVelocity;

    private float velThresh = 0.001f;
    // Update is called once per frame

    public void ChangeState(State state)
    {
        _state = state;
        if (!state)
        {
            var x = 3;
        }
        _state.Activate();
    }

    protected virtual void Start()
    {
        _physicsState = gameObject.GetComponent<PhysicsState>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();
        physicsEnabled = false;
        lastPos = transform.position;
    }
    
    protected virtual void Update()
    {
        // Debug.Log(_state.ToString());
        _state.Behaviour();
    }
    
    protected virtual void FixedUpdate()
    {
        if (physicsEnabled && this.gameObject.GetComponent<Rigidbody>().velocity.magnitude < velThresh) 
        {
            rb.isKinematic = true;
            agent.enabled = true;
            physicsEnabled = false;
        }
        lastVelocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
    }

    public void EnablePhysics()
    {
        ChangeState(_physicsState);
        agent.isStopped = true;
        rb.isKinematic = false;
        agent.enabled = false;
        physicsEnabled = true;
    }
    
    public void EnablePhysicsWithoutStateChange()
    {
        agent.isStopped = true;
        rb.isKinematic = false;
        agent.enabled = false;
        physicsEnabled = true;
    }
}
