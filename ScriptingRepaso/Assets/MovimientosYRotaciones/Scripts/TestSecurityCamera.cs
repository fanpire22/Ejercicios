using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSecurityCamera : MonoBehaviour {

    [SerializeField] private Transform _target;
    [SerializeField] private float _angularSpeed = 180;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 directionToTarget = _target.position - transform.position;
        transform.forward = Vector3.RotateTowards(transform.forward, directionToTarget, _angularSpeed * Mathf.Deg2Rad, 0);
	}
}
