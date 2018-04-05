using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraTrigger : MonoBehaviour {

    CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _camera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    // Use this for initialization
    void Start () {
        _camera.enabled = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        _camera.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _camera.enabled = false;
    }
}
