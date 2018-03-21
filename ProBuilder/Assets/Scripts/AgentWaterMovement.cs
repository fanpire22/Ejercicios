using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentWaterMovement : MonoBehaviour
{

    [SerializeField] float _speedReductionFactor = 0.25f;

    NavMeshAgent _agent;
    float _baseSpeed;

    // Use this for initialization
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _baseSpeed = _agent.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            _agent.speed = _baseSpeed * _speedReductionFactor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {

            _agent.speed = _baseSpeed;
        }
    }
}
