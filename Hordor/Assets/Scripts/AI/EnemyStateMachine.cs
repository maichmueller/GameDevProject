using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyStateMachine : MonoBehaviour
{
    // Start is called before the first frame update

    public State defaultState;
    private State _physicsState;
    public State _state;
    public bool physicsEnabled;
    public NavMeshAgent agent;
    public Rigidbody rb;
    public Vector3 lastPos;
    public Vector3 lastVelocity;
    private float waitTime;
    private float _elapsed;
    
    //Patch for grav gun
    public GravityGun gravGun;

    private float velThresh = 0.001f;
    // Update is called once per frame

    public void ChangeState(State state)
    {
        _state = state;
        //Debug.Log(_state.ToString());
        _state.Activate();
    }

    private void OnCollisionEnter(Collision other)
    {
        string[] tags = {"Player", "Enemy"};
        if (tags.Contains(other.gameObject.tag))
        {
            EnablePhysics();
        }
    }

    protected virtual void Start()
    {
        waitTime = 1f;
        _elapsed = 0f;
        _physicsState = gameObject.GetComponent<PhysicsState>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();
        physicsEnabled = false;
        lastPos = transform.position;
    }
    
    protected virtual void Update()
    {
        //Debug.Log(_state.ToString());
        _state.Behaviour();
    }
    
    protected virtual void FixedUpdate()
    {
        Debug.Log(physicsEnabled && lastPos == transform.position && GetComponent<PhysicsState>().reached && _elapsed > waitTime);
        if (physicsEnabled && lastPos == transform.position && GetComponent<PhysicsState>().reached && _elapsed > waitTime)
        {
            Debug.Log("Physics Disabled");
            rb.isKinematic = true;
            agent.enabled = true;
            physicsEnabled = false;
        }
        lastVelocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
        _state.FixedBehaviour();
        _elapsed += Time.deltaTime;
    }

    public void EnablePhysics()
    {
        if (!physicsEnabled)
        {
            Debug.Log("Physics Enable Called");
            ChangeState(_physicsState);
            _elapsed = 0f;
        }
    }
    
    public void EnablePhysicsWithoutStateChange()
    {
        agent.isStopped = true;
        rb.isKinematic = false;
        agent.enabled = false;
        physicsEnabled = true;
    }
}
