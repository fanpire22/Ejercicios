using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonBelmont : BaseCharacter
{
    [Header("Simon Simon!")]
    [SerializeField]
    private LayerMask _jumpingLayer;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private float _jumpForce;
    [SerializeField] Inventory _inventory;

    private float _horizontalAxis;
    private bool _bJumpPressed;
    private bool _bAttackPressed;
    private bool _bInAir;

    private Collider2D[] _footDetection = new Collider2D[5];

    public Inventory GetInventory()
    {
        return _inventory;
    }

    void UpdateJump()
    {
        base.ani.SetBool("OnAir", _bInAir);

        //Obtenemos la información sobre el estado de la animación del inicio del salto.
        AnimatorStateInfo state = ani.GetCurrentAnimatorStateInfo(0);

        //Estamos animando el salto y está a punto de terminar la animación
        if (state.IsName("Jump.JumpStart") && state.normalizedTime > 0.9f)
        {
            rig.velocity = new Vector2(rig.velocity.x, _jumpForce);
        }

    }

    protected override void Update()
    {
        base.Update();

        UpdateJump();


        _horizontalAxis = Input.GetAxis("Horizontal");
        _bJumpPressed = Input.GetButton("Jump");
        _bAttackPressed = Input.GetButton("Fire1");

        //Detectamos suelo por medio de una esfera ubicada en el punto de pivote (pies)
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, _detectionRadius, _footDetection, _jumpingLayer);
        _bInAir = count == 0;

        //Salto
        if (_bJumpPressed && !_bInAir)
        {
            base.ani.SetTrigger("Jump");
        }

        //Movimiento
        SimpleMove(Vector2.right * _horizontalAxis * _speedMovement);
        if (_horizontalAxis != 0)
        {
            spr.flipX = _horizontalAxis > 0;
        }
    }


#if UNITY_EDITOR

    protected override void DrawDebug()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _detectionRadius);
    }

#endif
}
