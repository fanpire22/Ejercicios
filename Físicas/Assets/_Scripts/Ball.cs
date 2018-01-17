using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{

    [SerializeField] private float fuerza = 10;
    [SerializeField] private float tiempo;
    [SerializeField] private Transform destino;

    void Start()
    {
        Invoke("Throw", tiempo);
    }

    void Throw()
    {
        transform.LookAt(destino);

        Vector3 impulso = transform.forward * fuerza;
        GetComponent<Rigidbody>().AddForce(impulso, ForceMode.Impulse);
    }

}
