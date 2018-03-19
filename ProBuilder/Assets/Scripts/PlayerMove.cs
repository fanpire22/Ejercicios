using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour {

    NavMeshAgent _agent;

	// Use this for initialization
	void Start () {
        _agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        _agent.Move(new Vector3(xInput, 0, zInput) * _agent.speed * Time.deltaTime);
    }
}
