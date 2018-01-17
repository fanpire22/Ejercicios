using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanque : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform _missileSpawner;
    [SerializeField] private GameObject _missile;
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _barrel;

    [Header("Propiedades")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _yawSpeed;
    [SerializeField]private float _maxYaw = 16;

    private Vector3 direction;
    private Vector3 directionBarrel;
    private Transform _drone;
    float _rotationAngle;
    float _lifetime = 0;

    private void Awake()
    {
        InvokeRepeating("Shoot", 1, 1);
        _drone = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        _lifetime += Time.deltaTime * _yawSpeed;
        direction = (_drone.position - _turret.position).normalized;
        directionBarrel = (_drone.position - _barrel.position).normalized;

        float angle = Vector3.SignedAngle(_turret.forward, directionBarrel, _turret.right);
        angle = Mathf.Clamp(angle, -_maxYaw, _maxYaw);

        _rotationAngle = - Mathf.Lerp(_barrel.localEulerAngles.x, angle, _lifetime);
    }

    private void FixedUpdate()
    {
        Vector3 turretDirection = new Vector3(direction.x, 0, direction.z);
        turretDirection = Vector3.RotateTowards
            (
                _turret.forward,
                turretDirection,
                _turnSpeed * Mathf.Deg2Rad * Time.deltaTime,
                1
            );

        _turret.rotation = Quaternion.LookRotation(turretDirection);


        _barrel.localRotation = Quaternion.Euler(_rotationAngle, 0, 0);
    }

    private void Shoot()
    {
        Instantiate(_missile, _missileSpawner.position, _missileSpawner.rotation);
    }
}
