using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Reposition : State
{
    public Shoot shoot;
    public Transform player;
    public NavMeshAgent agent;
    
    
    public override void Activate()
    {
        agent.isStopped = false;
        shoot = gameObject.GetComponent<Shoot>();
        if (!_machine.physicsEnabled) agent.SetDestination(player.position);
    }

    public override void Behaviour()
    {
        gameObject.transform.LookAt(player);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                agent.isStopped = true;
                _machine.ChangeState(shoot);
            }
        }
    }
}
