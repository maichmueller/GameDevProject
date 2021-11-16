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
        if (!_machine.physicsEnabled) agent.SetDestination(player.position);
    }

    public override void Behaviour()
    {
        if (!_machine.physicsEnabled) agent.SetDestination(player.position);
    }
}
