using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float SpeedMovement;
    Animator _animator;
    Rigidbody2D _rb;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _animator.SetFloat("Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        if(horizontal != 0 || vertical != 0)
        {
            _animator.SetFloat("Horizontal", horizontal);
            _animator.SetFloat("Vertical", vertical);

            Vector2 direction = new Vector2(horizontal, vertical);
            _rb.MovePosition(_rb.position + direction * Time.deltaTime * SpeedMovement);
        }
    }
}
