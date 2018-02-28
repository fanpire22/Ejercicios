using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : WeaponBase {

    protected override void OnHealthOverlap(List<HealthManager> colliders)
    {
        base.OnHealthOverlap(colliders);
        foreach(HealthManager hm in colliders)
        {
            hm.Damage(this);
        }

        Destroy(gameObject);
    }
}
