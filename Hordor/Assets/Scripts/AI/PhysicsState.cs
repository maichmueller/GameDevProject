using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PhysicsState : State
{
    // Start is called before the first frame update

    public bool reached;

    public override void Activate()
    {
        _machine.agent.isStopped = true;
        _machine.rb.isKinematic = false;
        _machine.agent.enabled = false;
        _machine.physicsEnabled = true;
        reached = true;
        Debug.Log("Physics Enabled");
    }

    public override void Behaviour()
    {
        //if(!_machine.physicsEnabled) _machine.ChangeState(_machine.defaultState);
    }

    public override void FixedBehaviour()
    {
        if (!_machine.physicsEnabled)
        {
            reached = false;
            Debug.Log("Physics State Switched");
            _machine.ChangeState(_machine.defaultState);
        }
    }
}
