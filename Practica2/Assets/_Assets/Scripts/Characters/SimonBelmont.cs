using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SimonBelmont : BaseCharacter
{
    [Header("Simon Simon!")]
    [SerializeField]
    private LayerMask _jumpingLayer;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private float _jumpForce;
    [SerializeField] Inventory _inventory;
    [Tooltip("0: Látigo, 1: Daga, 2: Boomerang, 3: Hacha")]
    [SerializeField]
    WeaponBase[] _weaponList;
    [SerializeField] Transform _weaponSource;

    public static bool bRestoreLocation;
    public static Vector3 RestoreLocation;

    int _currentWeapon;

    private float _horizontalAxis;
    private bool _bJumpPressed;
    private bool _bAttackPressed;
    private bool _bInAir;


    private Collider2D[] _footDetection = new Collider2D[5];
#if UNITY_EDITOR
    private void Start()
    {

        _inventory.AddGold(5000);
    }
#endif
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

        if (_bAttackPressed)
        {
            base.Attack();

            //switch (_currentWeapon)
            //{
            //    case 0:
            //        //Whip
            //        break;
            //    case 1:
            //        //Dagger
            //        break;
            //    case 2:
            //        //Boomerang
            //        break;
            //    case 3:
            //        //Axe
            //        break;
            //    default:
            //        break;
            //}
        }
        if (!_bAttackPressed && Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(0);
        if (!_bAttackPressed && Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(1);
        if (!_bAttackPressed && Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeapon(2);
        if (!_bAttackPressed && Input.GetKeyDown(KeyCode.Alpha4)) ChangeWeapon(3);
    }

    private void ChangeWeapon(int index)
    {
        if (index == 0 || _inventory.HasItem((byte)index))
        {
            _currentWeapon = index;

        }
    }


    protected override void OnAttack()
    {

        base.ani.SetInteger("Weapon", _currentWeapon);
        base.ani.SetTrigger("Attack");

        float direction = (spr.flipX ? 1 : -1);

        WeaponBase baseW = GameObject.Instantiate(_weaponList[_currentWeapon]);
        baseW.Initialize(gameObject, direction);
        baseW.transform.position = _weaponSource.transform.position + Vector3.right * _weaponList[_currentWeapon].SpawnOffset * direction;
    }

    public override void OnDeath()
    {
        string Json = File.ReadAllText(string.Format("{0}{1}", FSaveData.FullPath, FSaveData.FileName));
        FSaveData datos = JsonUtility.FromJson<FSaveData>(Json);

        HealthManager.Heal(int.MaxValue);
        rig.MovePosition(datos.Position);
        _inventory.RemoveGold(int.MaxValue);

        print("<size=16><color=red>¡SIMON, TU TALADRO PERFORARÁ EL CIELO! ...o no</color></size>");
    }

#if UNITY_EDITOR

    protected override void DrawDebug()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _detectionRadius);
    }

#endif
}
