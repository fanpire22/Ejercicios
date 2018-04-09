using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

    NavMeshAgent _agent;

    public void GoToDestination(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
}
