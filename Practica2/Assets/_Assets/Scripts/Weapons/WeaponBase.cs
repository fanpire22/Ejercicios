using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FDamageWeapon
{
    public EDamageTypes type;
    public int amount;
}

public class WeaponBase : MonoBehaviour
{
    [SerializeField] List<FDamageWeapon> Damages;

    /// <summary>
    /// Control de las físicas
    /// </summary>
    private void FixedUpdate()
    {
        Collider2D[] colliders = null;

        //Creamos un sistema de filtros para el tipo "Damageable"
        ContactFilter2D Params = new ContactFilter2D();
        Params.layerMask = 1 << LayerMask.NameToLayer("Damageable");

        Physics2D.OverlapCollider(GetComponent<Collider2D>(), Params, colliders);

        if (colliders != null && colliders.Length > 0)
        {
            HealthManager[] auxArray = new HealthManager[colliders.Length];
            for(int i = 0; i < colliders.Length; i++)
            {
                auxArray[i] = colliders[i].GetComponent<HealthManager>();
            }
            
            OnHealthOverlap(auxArray);
        }
    }

    /// <summary>
    /// Función que se llama cuando se colisiona con un objetivo
    /// </summary>
    protected virtual void OnHealthOverlap(HealthManager[] colliders)
    {

    }

    /// <summary>
    /// Las clases hijas deben implementar el comportamiento específico de cada arma aquí
    /// </summary>
    protected virtual void Update()
    {

    }
}
