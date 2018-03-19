using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{

    [SerializeField] Transform _target;
    NavMeshAgent _agent;

    // Use this for initialization
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_target.position);
    }
}
