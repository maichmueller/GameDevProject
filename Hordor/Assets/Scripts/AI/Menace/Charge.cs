using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charge : State
{
    public AudioClip scream;
    private GameObject _player;
    private Vector3 chargePos;
    private NavMeshAgent _agent;
    public float cooldown;
    public float chargeWait;
    public float power;
    public float acceleration;
    private float _defaultAcceleration;
    private float _elapsed;
    private AudioSource audio;

    public override void Activate()
    {
        audio = gameObject.GetComponent<AudioSource>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _agent.isStopped = true;
        _agent.ResetPath();
        _agent.isStopped = false;
        _elapsed = 0f;
        _player = GameObject.FindWithTag("Player");
        chargePos = _player.transform.position + (-_player.transform.forward * 2);
        gameObject.transform.LookAt(chargePos);
        _agent.acceleration += acceleration;
        _agent.speed += power;
        //audio.PlayDelayed(chargeWait);
        //Debug.Log("LOOK AT");
        //chargePos = transform.forward;
        //StartCoroutine(nameof(StartCharge));
        //_machine.EnablePhysicsWithoutStateChange();
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * power);
        //gameObject.transform.Translate(transform.forward * power);
    }

    public override void Behaviour()
    {
    }

    public override void FixedBehaviour()
    {
        if (_elapsed > chargeWait && _elapsed < chargeWait + 0.1f)
        {
            _agent.SetDestination(chargePos);
            audio.clip = scream;
            audio.Play();
        }
        if (_elapsed > cooldown + chargeWait)
        {
            _agent.speed -= power;
            _agent.acceleration -= acceleration;
            _machine.ChangeState(gameObject.GetComponent<Beeline>());
        }
        _elapsed += Time.deltaTime;
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Player") && _machine._state == GetComponent<Charge>() && _elapsed < chargeTime)
    //     {
    //         Debug.Log("Triggered");
    //         _elapsed = chargeTime;
    //     }
    // }

}
