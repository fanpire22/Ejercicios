using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase {
    [SerializeField] float _speedLinear;
    [SerializeField] float _speedAngular;
    [SerializeField] int _maxHits;

    int _currentHits;

    protected override void Update()
    {
        transform.position += _direction * Vector3.right * _speedLinear * Time.deltaTime;
        transform.Rotate(Vector3.forward * _speedAngular * Time.deltaTime * -_direction);
    }

    protected override void OnHealthOverlap(List<HealthManager> colliders)
    {
        List<HealthManager> aux = new List<HealthManager>();
        foreach (HealthManager hm in colliders)
        {
            if (!aux.Contains(hm))
            {
                if (_currentHits < _maxHits)
                {
                    //No ha sido golpeado antes, y hay que darle a un nuevo objeto
                    hm.Damage(this);
                    aux.Add(hm);
                    _currentHits++;
                }
            }
        }
        if (_currentHits == _maxHits) Destroy(this.gameObject);
    }
}
