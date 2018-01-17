using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLerp : MonoBehaviour {

    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _destination;
    [SerializeField] private float _interpolationDuration;

    private float _interpolationPercent;
    private float _interpolationSpeed;

    private bool _movingToDestination = true;

    // Use this for initialization
    void Start () {
        _interpolationPercent = 0;
        _interpolationSpeed = 1 / _interpolationDuration;
	}
	
	// Update is called once per frame
	void Update () {

        if (_movingToDestination)
        {
            _interpolationPercent += _interpolationSpeed * Time.deltaTime;
            if (_interpolationPercent >= 1) _movingToDestination = false;
        }
        else
        {
            _interpolationPercent -= _interpolationSpeed * Time.deltaTime;
            if (_interpolationPercent <= 0) _movingToDestination = true;
        }
        this.transform.position = Vector3.Lerp(_origin.position, _destination.position, _interpolationPercent);

    }
}
