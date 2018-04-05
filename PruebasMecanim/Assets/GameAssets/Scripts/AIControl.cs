using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{

    [SerializeField] Transform _waypointsRoot;

    Transform[] _wayPoints;
    int _currentWayPoint = 0;
    NavMeshAgent _navi;
    

    // Use this for initialization
    void Awake()
    {
        _navi = GetComponent<NavMeshAgent>();

        int nChildren = _waypointsRoot.childCount;

        _wayPoints = new Transform[nChildren];

        for(int i = 0; i < nChildren; i++)
        {
            _wayPoints[i] = _waypointsRoot.GetChild(i);
        }
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_navi.pathPending && _navi.remainingDistance < 0.2f)
        {
            //Hay que cambiar de punto
            _currentWayPoint = (_currentWayPoint + 1) % _waypointsRoot.childCount;
            _navi.SetDestination(_wayPoints[_currentWayPoint].position);
        }
    }
}
