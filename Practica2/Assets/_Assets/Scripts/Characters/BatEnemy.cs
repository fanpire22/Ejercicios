using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : BaseCharacter {

    [SerializeField] Transform _homeTransform;
    [SerializeField] float _aggroDistance;
    [SerializeField] float _minAttackRange;
    [SerializeField] int _damage;
    WeaponBase fangs;

    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();
        fangs = new WeaponBase();
        fangs.damages.Add(new FDamageWeapon
        {
            amount = _damage,
            type = EDamageTypes.Piercing
        });
    }
	
	// Update is called once per frame
	protected override void Update () {
        Vector2 dirToSimon = GameManager.instance.getSimonSimon().transform.position - transform.position;

        if(dirToSimon.magnitude < _aggroDistance)
        {
            if (_minAttackRange > dirToSimon.magnitude)
            {
                Attack();
            }
            else
            {
                //Vamos a por Simon
                Move(dirToSimon.normalized * _speedMovement);
            }
        }
        else
        {
            Vector2 dirToHome = _homeTransform.position - transform.position;

            if(dirToHome.magnitude < 0.02f)
            {
                rig.MovePosition(_homeTransform.position);
                ani.SetFloat("Speed", 0f);
            }
            else
            {
                Move(dirToHome.normalized * _speedMovement);
            }
        }
    }

    public override void OnDeath()
    {
        GameManager.instance.getSimonSimon().GetInventory().AddGold(5);

        Destroy(gameObject);
    }

    protected override void OnAttack()
    {
        GameManager.instance.getSimonSimon().HealthManager.Damage(fangs);
    }
}
