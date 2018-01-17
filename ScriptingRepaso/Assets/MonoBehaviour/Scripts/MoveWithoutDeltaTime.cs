using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithoutDeltaTime : MonoBehaviour {

    [SerializeField] private float _speed = 5;
    	
	// Update is called once per frame
	void FixedUpdate () {
        if (Time.time < 3)
        {
            //System.Threading.Thread.Sleep(Random.Range(10,50));
            this.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            Debug.Log(string.Format("posición: {0}", this.transform.position.z));
        }
	}
}
