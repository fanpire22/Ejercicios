using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Soldier
{
    
    protected NavMeshAgent _agent;

    private void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void GoToDestination(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

}
