using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimator : MonoBehaviour
{

    Animator _ani;
    NavMeshAgent _navi;

    // Use this for initialization
    void Awake()
    {
        _ani = GetComponent<Animator>();
        _navi = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 agentSpeed = _navi.velocity;

        agentSpeed = transform.InverseTransformVector(agentSpeed);
        _ani.SetFloat("speedX", agentSpeed.x);
        _ani.SetFloat("speedZ", agentSpeed.z);
    }
}
