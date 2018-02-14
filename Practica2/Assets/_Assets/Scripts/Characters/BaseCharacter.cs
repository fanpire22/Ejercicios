using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{

    [SerializeField] [Range(1, 10)] protected float _speedMovement;

    public Animator ani { get; protected set; }
    public Rigidbody2D rig { get; protected set; }
    public HealthManager HealthManager { get; protected set; }
    public SpriteRenderer spr { get; protected set; }

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

    private void Awake()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    protected void Move(Vector2 direction)
    {

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
}
