using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected EnemyStateMachine _machine;

    private void Start()
    {
        _machine = gameObject.GetComponent<EnemyStateMachine>();
    }

    public abstract void Activate();

    public abstract void Behaviour();

}
