using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIToAnimator : MonoBehaviour {

    private NavMeshAgent _agent;
    private Animator _ani;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 agentVelocity = this.transform.InverseTransformVector(_agent.velocity);

        _ani.SetFloat("SpeedX", agentVelocity.x);
        _ani.SetFloat("SpeedZ", agentVelocity.z);

    }
}
