using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    private bool _bIsDamaged = false;

    private void Awake()
    {
        Renderer renderizador;

        for (int i = 0; i < transform.childCount; i++)
        {
            //Le asignamos una copia del material de nuestro color, y le ponemos un color al azar
            renderizador = transform.GetChild(i).GetComponent<Renderer>();
            renderizador.material = new Material(renderizador.material);

            renderizador.material.color = Random.ColorHSV(0, 1, 0.8f, 0.8f, 1, 1);
        }
    }

    /// <summary>
    /// Lanza cajas en una dirección al azar
    /// </summary>
    public void Damage()
    {
        if (!_bIsDamaged)
        {
            _bIsDamaged = true;

            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 force = Random.onUnitSphere * 100;
                Vector3 position = transform.position;

                Rigidbody rig = transform.GetChild(i).GetComponent<Rigidbody>();

                rig.isKinematic = false;
                rig.AddForceAtPosition(force, position);

                Destroy(transform.GetChild(i).gameObject, 5f);
            }
        }
        Destroy(gameObject, 5.5f);
    }
    
}
