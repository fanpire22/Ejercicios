using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _floorDetectionDistance;
    [SerializeField] float _maxVelocityMagnitude;
    [SerializeField] Vector3 _jumpOffset;

    Rigidbody2D _rb;

    float _horizontal;
    bool _jump;
    bool _bIsJumping;
    Camera mainCamera;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _jump = Input.GetButton("Jump");

        _rb.AddTorque(-_horizontal * _speed * Time.deltaTime);

        float radius = GetComponent<CircleCollider2D>().radius;
        Vector3 baseLoc = transform.position + Vector3.down * radius + _jumpOffset;

        //Detectamos si hay suelo debajo de la esfera
        RaycastHit2D hit = Physics2D.Raycast(baseLoc, Vector2.down, _floorDetectionDistance);
        Debug.DrawRay(baseLoc, Vector3.down * _floorDetectionDistance, Color.red);

        if(_jump && hit)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            _bIsJumping = true;
        }

        if (_bIsJumping)
        {
            //Clamp magnitude
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxVelocityMagnitude);
            _bIsJumping = !(!_jump && hit);
        }

        Vector3 CameraPosition = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = CameraPosition;

    }

}
