using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCharacter : BaseCharacter
{
    [SerializeField] float _minDistanceAttack;
    [SerializeField] Fireball _fireball;
    [SerializeField] Transform _spawn1;
    [SerializeField] Transform _spawn2;

    Vector3 _direction;
    bool _bInAttackBehaviour;

    protected override void Update()
    {
        base.Update();

        Transform playerTransform = GameManager.instance.getSimonSimon().transform;
        _direction = playerTransform.position - transform.position;
        _bInAttackBehaviour = _direction.magnitude < _minDistanceAttack;
    }

    protected override void OnAttack()
    {
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GameManager.instance.getSimonSimon().GetInventory().AddGold(100);
        Destroy(gameObject);
    }

    public void Spawn1()
    {
        if(_bInAttackBehaviour)
        {
            Fireball fireball = Instantiate(_fireball, _spawn1.transform.position, Quaternion.identity);
            fireball.Initialize(gameObject);
            fireball.DirectionFireball = _direction.normalized * 3;
        }
    }

    public void Spawn2()
    {
        if (_bInAttackBehaviour)
        {
            Fireball fireball = Instantiate(_fireball, _spawn2.transform.position, Quaternion.identity);
            fireball.Initialize(gameObject);
            fireball.DirectionFireball = _direction.normalized * 3;
        }
    }
}
