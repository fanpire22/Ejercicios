using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BakeNavMesh : MonoBehaviour
{

    NavMeshSurface _surface;

    // Use this for initialization
    private void Awake()
    {
        _surface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        _surface.BuildNavMesh();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _surface.BuildNavMesh();
        }
    }
}
