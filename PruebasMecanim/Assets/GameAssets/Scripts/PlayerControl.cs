using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [SerializeField] float _walkSpeed = 1.5f;
    [SerializeField] float _runSpeed = 3.5f;
    [SerializeField] float _jumpForce = 10;

    Animator _animator;
    Rigidbody _rigidbody;

    bool _isGrounded = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    
	void Update () {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        bool runInput = Input.GetKey(KeyCode.LeftShift);
        bool crouchInput = Input.GetKey(KeyCode.LeftControl);
        bool aimInput = Input.GetMouseButton(1);
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);

        // Equivalente al if de más abajo, pero con operador ternario
        /*float usedSpeed = runInput ? _runSpeed : _walkSpeed;        
        _animator.SetFloat("speedZ", zInput * usedSpeed);
        _animator.SetFloat("speedX", xInput * usedSpeed);*/

        if (runInput)
        {
            _animator.SetFloat("speedZ", zInput * _runSpeed);
            _animator.SetFloat("speedX", xInput * _runSpeed);
        }
        else
        {
            _animator.SetFloat("speedZ", zInput * _walkSpeed);
            _animator.SetFloat("speedX", xInput * _walkSpeed);
        }

        _animator.SetBool("crouch", crouchInput);

        if (aimInput) { _animator.SetLayerWeight(1, 1); }
        else { _animator.SetLayerWeight(1, 0); }

        if(jumpInput && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _animator.SetTrigger("jump");            
        }

        _animator.SetBool("isGrounded", _isGrounded);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
    }


}
