using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseCharacter : MonoBehaviour
{


    [SerializeField] [Range(1, 10)] protected float _speedMovement;
    public Animator ani { get; protected set; }
    public Rigidbody2D rig { get; protected set; }
    public HealthManager HealthManager { get; protected set; }
    public SpriteRenderer spr { get; protected set; }

    [Header("Combat")]
    [SerializeField] float _attackRate;
    protected float _nextTimeCanAttack;

#if UNITY_EDITOR
    [Header("Develop")]
    [SerializeField] bool bDrawDebug;

    protected virtual void DrawDebug()
    {

    }

#endif

    protected void OnDrawGizmos()
    {

#if UNITY_EDITOR
        if (bDrawDebug) DrawDebug();
#endif
    }

    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        HealthManager = GetComponent<HealthManager>();
    }

    protected void Move(Vector2 direction)
    {
        rig.velocity = direction;
        ani.SetFloat("Speed", direction.magnitude);
    }

    protected void SimpleMove(Vector2 direction)
    {
        rig.velocity= new Vector2(direction.x, rig.velocity.y);
        ani.SetFloat("Speed", Mathf.Abs(direction.x));
    }

    protected void Jump()
    {

    }

    protected virtual void Update()
    {

    }

    protected bool Attack()
    {
        if (Time.time > _nextTimeCanAttack)
        {
            _nextTimeCanAttack = Time.time + _attackRate;
            OnAttack();
            return true;
        }
        return false;
    }

    public virtual void OnDeath()
    {

    }

    protected abstract void OnAttack();
}
