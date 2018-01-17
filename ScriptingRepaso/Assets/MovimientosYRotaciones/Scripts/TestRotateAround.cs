using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotateAround : MonoBehaviour {

    [SerializeField] private GameObject _rotateAround;
    [SerializeField] private float _angularSpeed = 360;
    private float _distance;

    private float _baseY;

	// Use this for initialization
	void Start () {
        _baseY = this.transform.position.y;
        _distance = Vector3.Distance(this.transform.position, _rotateAround.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = this.transform.position;

        position.y = _baseY + Mathf.Sin(Time.time * 4 ) / 2;
        this.transform.position = position;

        Vector3 directionFromTarget = this.transform.position - _rotateAround.transform.position;
        directionFromTarget.Normalize();
        this.transform.position = _rotateAround.transform.position + directionFromTarget * _distance;

        this.transform.RotateAround(_rotateAround.transform.position, Vector3.up, _angularSpeed * Time.deltaTime);
	}
}
