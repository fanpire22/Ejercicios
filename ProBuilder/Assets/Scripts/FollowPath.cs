using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : MonoBehaviour
{

    [SerializeField] Transform[] _pathPoints;
    int _currentPathPointIndex = 0;

    NavMeshAgent _agent;

    // Use this for initialization
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Transform pathPoint = _pathPoints[_currentPathPointIndex];
        _agent.SetDestination(pathPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= 0.5f)
        {
            _currentPathPointIndex = (_currentPathPointIndex + 1) % _pathPoints.Length;
            Transform pathpoint = _pathPoints[_currentPathPointIndex];
            _agent.SetDestination(pathpoint.position);
        }
    }
}
