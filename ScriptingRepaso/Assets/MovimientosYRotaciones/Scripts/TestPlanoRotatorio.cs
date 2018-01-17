using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlanoRotatorio : MonoBehaviour {
    	
	// Update is called once per frame
	void Update () {

        float xRotation = Input.GetAxis("Vertical");
        float zRotation = Input.GetAxis("Horizontal");

        this.transform.rotation *= Quaternion.Euler(xRotation, 0, zRotation);

    }
}
