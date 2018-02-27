using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : WeaponBase {

    [Header("Dagger Properties")]
    [SerializeField] float speed; 

    /// <summary>
    /// Movemos la daga en la dirección requerida con la velocidad específica
    /// </summary>
    protected override void Update()
    {
        transform.position += base._direction * Vector3.right * speed * Time.deltaTime;
    }

    /// <summary>
    /// Al colisionar con un objeto dañable, nos destruimos
    /// </summary>
    /// <param name="colliders">Objetos dañables con los que nos hemos chocado</param>
    protected override void OnHealthOverlap(List<HealthManager> colliders)
    {
        colliders[0].Damage(this);
        Destroy(this.gameObject);
    }
}
