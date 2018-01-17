using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misil : MonoBehaviour
{

    [SerializeField] private float _speed = 10;
    [SerializeField] private GameObject _explosionPrefab;
    private Rigidbody _rig;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        Invoke("Despawn", 5);
    }

    private void FixedUpdate()
    {
        _rig.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CancelInvoke();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Despawn();
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
