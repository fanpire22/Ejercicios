using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : Enemy
{

    [SerializeField] float _speed;
    [SerializeField] float _rateRandomRotation;
    [SerializeField] float _rof;
    [SerializeField] float _damage;
    [SerializeField] float _minDistanceAttack;
    [SerializeField] float _minDistanceLook;
    [SerializeField] float _timeRandomBehaviour;

    private CharacterController _chara;
    private float _timeStamp;
    private Transform _playerLocation;
    private float _timeMovementRandom;

    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();
        _playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {

        if (_playerLocation)
        {
            Vector3 direction = _playerLocation.position - transform.position;
            float distance = direction.magnitude;

            if (distance < _minDistanceAttack)
            {
                //Attack
                _timeMovementRandom = 0;
            }
            else if (distance < _minDistanceLook)
            {
                //Look & Follow
                _timeMovementRandom = 0;
            }
            else
            {
                //RandomBehaviour
                _timeMovementRandom += Time.deltaTime;
                if (_timeMovementRandom > _timeRandomBehaviour)
                {
                    _timeMovementRandom = 0;
                    transform.rotation = Quaternion.Euler(0, (Random.Range(0.0f, 359.9f)), 0);
                }
                _chara.SimpleMove(transform.forward * _speed);
            }
        }
    }

    private void Shoot()
    {

    }
}
