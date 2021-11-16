using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : State
{
    private GameObject _player;
    public float cooldown;
    public float power;
    private float _elapsed;
    
    public override void Activate()
    {
        _elapsed = 0f;
        _player = GameObject.FindWithTag("Player");
        gameObject.transform.LookAt(_player.transform);
        _machine.EnablePhysicsWithoutStateChange();
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * power);
    }

    public override void Behaviour()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed > cooldown && !_machine.physicsEnabled) _machine.ChangeState(gameObject.GetComponent<Beeline>());
    }
    
}
