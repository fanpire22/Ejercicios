using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCharacter : BaseCharacter {
    protected override void OnAttack()
    {

    }

    protected override void Update()
    {
        base.Update();

        Transform playerTransform = GameManager.instance.getSimonSimon().transform;

    }
    public override void OnDeath()
    {
        base.OnDeath();
    }
}
