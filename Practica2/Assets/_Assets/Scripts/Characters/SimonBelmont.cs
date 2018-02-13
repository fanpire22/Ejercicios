using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonBelmont : BaseCharacter
{
    private float _horizontalAxis;
    private bool _bJumpPressed;
    private bool _bAttackPressed;

    private Collider2D[] _footDetection = new Collider2D[5];
    private bool _bInAir;

    protected override void Update()
    {
        base.Update();

        _horizontalAxis = Input.GetAxis("Horizontal");
        _bJumpPressed = Input.GetButton("Jump");
        _bAttackPressed = Input.GetButton("Fire1");

        //Detectamos suelo por medio de una esfera ubicada en el punto de pivote (pies)
        Physics2D.OverlapCircleNonAlloc(transform.position,0.2f,_footDetection);
    }

}
