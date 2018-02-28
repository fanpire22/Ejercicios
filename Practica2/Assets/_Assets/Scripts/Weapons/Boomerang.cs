using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : WeaponBase {

    [Header("Boomerang Properties")]
    [SerializeField] float speed;
    [SerializeField] float distance;
    Vector3 returnPoint;

    bool bArrived = false;
    float overrideDirection = 1;

    protected override void Update()
    {
        if(!bArrived && Vector3.Distance(transform.position, returnPoint) < 0.01f)
        {
            //Volvemos
            bArrived = true;
            overrideDirection = -1;
        }
        transform.position += Vector3.right * _direction * overrideDirection * speed * Time.deltaTime;
    }

    /// <summary>
    /// Daña al primer objeto con el que colisiona, pero aún así retorna al usuario
    /// </summary>
    /// <param name="colliders"></param>
    protected override void OnHealthOverlap(List<HealthManager> colliders)
    {
        colliders[0].Damage(this);
    }

    /// <summary>
    /// El boomerang no dispone de tiempo de destrucción. Vuelve al usuario y se destruye
    /// </summary>
    protected override void Start()
    {
        base.Start();
        returnPoint = transform.position += Vector3.right * _direction *  distance;
        Debug.DrawLine(returnPoint, transform.position);
    }
}
