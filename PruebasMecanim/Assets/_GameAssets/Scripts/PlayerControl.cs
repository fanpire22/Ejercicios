using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    Animator _ani;
    private const float RunSpeed = 2.5f;
    private const float WalkSpeed = 1.5f;

    // Use this for initialization
    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        bool runInput = Input.GetKey(KeyCode.LeftShift);
        bool crouchInput = Input.GetKey(KeyCode.LeftControl);
        bool aimInput = Input.GetMouseButton(1);

        float speed = (runInput ? RunSpeed : WalkSpeed);

        _ani.SetFloat("ForwardSpeed", zInput * speed);
        _ani.SetFloat("LateralSpeed", xInput * speed);
        _ani.SetBool("Crouch", crouchInput);

        _ani.SetLayerWeight(1, aimInput ? 1 : 0);
    }
}
