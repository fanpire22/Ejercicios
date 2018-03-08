using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : WeaponBase {

    public Vector3 Target;
    public float TorqueSpeed = 80;

    private float _fuel = 100;
    private float _consumptionOnMove = 1;
    private float _consumptionOnHit = 10;

    FDamageWeapon dmg = new FDamageWeapon()
    {
        amount = 1,
        type = EDamageTypes.Fire
    };

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        transform.position = Target * Time.deltaTime;
        transform.Rotate(Vector3.forward * TorqueSpeed * Time.deltaTime);
        _fuel -= _consumptionOnMove;
    }


    protected override void OnHealthOverlap(List<HealthManager> colliders)
    {
        base.OnHealthOverlap(colliders);

        //Perdemos combustible por golpe
        for (int i = 0; i < colliders.Count && _fuel > 0; i++)
        {
            _fuel -= _consumptionOnHit;
            colliders[i].Damage(dmg);
        }
        if (_fuel < 0)
        {
            Destroy(gameObject);
        }
    }
}
