using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour {

    private void Awake()
    {
        Debug.Log("Awake with name: " + this.gameObject.name);
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Start with name: " + this.gameObject.name);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Update " + Time.deltaTime);
    }
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate " + Time.deltaTime);
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
