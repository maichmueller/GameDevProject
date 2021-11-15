using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MenaceStateMachine : EnemyStateMachine
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _state = gameObject.GetComponent<Beeline>();
        _state.Activate();
    }
    
}
