using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTranslate : MonoBehaviour {

    [SerializeField] private float _speed= 5;
    [SerializeField] private float _angularSpeed = 180;
    private Camera camara;

    private void Start()
    {
        camara = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update () {
        float xMovement = Input.GetAxis("Horizontal") * _angularSpeed;
        float zMovement = Input.GetAxis("Vertical") * _speed;

        this.transform.Translate(Vector3.forward * zMovement * Time.deltaTime);
        this.transform.Rotate(new Vector3(0, xMovement * Time.deltaTime, 0));

        camara.transform.LookAt(this.transform);

    }
}
