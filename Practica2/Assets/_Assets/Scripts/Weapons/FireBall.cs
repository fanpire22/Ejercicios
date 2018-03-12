using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : WeaponBase
{
    public Vector3 DirectionFireball { get; set; }
    private float Fuel = 100;
    

    protected override void OnHealthOverlap(List<HealthManager> colliders)
    {
        base.OnHealthOverlap(colliders);

        for(int i = 0; i < colliders.Count && Fuel > 0; i++)
        {
            //FDamageWeapon damage = new FDamageWeapon()
            //{
            //    AmmountDamage = 1,
            //    Type = EDamageTypes.Bludgeoning
            //};
            colliders[i].Damage(this);
            Fuel -= 10;
        }

        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        transform.position += DirectionFireball * Time.deltaTime;
        transform.Rotate(Vector3.forward * 80 * Time.deltaTime);
    }

    
}
