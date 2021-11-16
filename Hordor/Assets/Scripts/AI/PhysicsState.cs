using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsState : State
{
    // Start is called before the first frame update

    public override void Activate()
    {
    }

    public override void Behaviour()
    {
        if(!_machine.physicsEnabled) _machine.ChangeState(_machine.defaultState);
    }
    
}
