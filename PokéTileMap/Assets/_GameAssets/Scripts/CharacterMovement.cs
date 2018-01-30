using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private enum EGenre { Female, Male };
    [Header("Miscelaneous")]
    [SerializeField]
    private EGenre Oak = EGenre.Male;

    [Header("Speed")]
    [SerializeField]
    float _speedWalk;
    [SerializeField] float _speedRun;
    [SerializeField] float _speedBike;

    [Header("Inventory")]
    public bool bHasBike;

    Animator _animator;
    Rigidbody2D _rb;

    float _horizontal;
    float _vertical;

    float currentSpeed;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _animator.SetBool("Sex", Oak == EGenre.Male);
    }

    // Update is called once per frame
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        float Speed = Mathf.Abs(_horizontal) + Mathf.Abs(_vertical);
        if (Speed > 0)
        {
            _animator.SetFloat("Horizontal", _horizontal);
            _animator.SetFloat("Vertical", _vertical);
            _animator.SetFloat("Speed", GetSpeed(Input.GetKey(KeyCode.LeftShift)));
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(_horizontal, _vertical);

        _rb.MovePosition(_rb.position + direction * currentSpeed * Time.fixedDeltaTime);
    }

    private float GetSpeed(bool bIsRunning)
    {
        if (!bIsRunning)
        {
            currentSpeed = _speedWalk;
            return 1;
        }

        if (!bHasBike)
        {
            currentSpeed = _speedRun;
            return 2;
        }
        currentSpeed = _speedBike;
        return 3;
    }
}
