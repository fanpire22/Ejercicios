using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWarhead : MonoBehaviour {

    public int Damage;

    private void OnParticleCollision(GameObject other)
    {
        Damageable dmg = other.GetComponent<Damageable>();

        if (dmg)
        {
            dmg.GetDamage(Damage, 0);
        }

        Destroy(gameObject);
    }
}
