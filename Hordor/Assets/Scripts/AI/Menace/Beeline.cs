using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beeline : State
{

    public Transform player;
    public NavMeshAgent agent;
    
    public override void Activate()
    {
        agent.SetDestination(player.position);
    }

    public override void Behaviour()
    {
        agent.SetDestination(player.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 5))
        {
            _machine.ChangeState(gameObject.GetComponent<Charge>());
        }
    }
}
