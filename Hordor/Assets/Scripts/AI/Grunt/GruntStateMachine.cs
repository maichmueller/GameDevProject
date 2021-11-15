using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntStateMachine : EnemyStateMachine
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _state = gameObject.GetComponent<Shoot>();
        _state.Activate();
    }

}
