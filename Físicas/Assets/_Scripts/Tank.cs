using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

    [SerializeField] private Transform _missileSpawner;
    [SerializeField] private GameObject _missile;
    [SerializeField] private GameObject _turret;
    [SerializeField] private GameObject _cannon;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _yawSpeed;

    private float _maxYawUp = 80;
    private float _maxYawDown = -30;



    private void Awake()
    {
        InvokeRepeating("Shoot", 1, 1);
    }

    private void FixedUpdate()
    {

    }

    private void Shoot()
    {
        Instantiate(_missile, _missileSpawner.position, _missileSpawner.rotation);
    }
}
