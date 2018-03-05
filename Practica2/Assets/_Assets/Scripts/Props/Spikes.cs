using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spikes : MonoBehaviour
{
    [SerializeField] float MaxMove;
    [SerializeField] float Time;
    [SerializeField] int Damage;
    [SerializeField] float pullBackForce;

    /// <summary>
    /// Hacemos que los pinchos suban y bajen si se han definido MaxMove y Time.
    /// </summary>
    void Start()
    {
        float movement = MaxMove + transform.position.y;
        transform.DOMoveY(movement, Time).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// En caso de colisionar con los pinchos, si es el jugador le dañamos y le aplicamos un pullBackForce para sacarlo.
    /// Si no queremos que salga del trigger, no le ponemos PullbackForce
    /// </summary>
    /// <param name="collision">jugador</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FDamageWeapon dmg = new FDamageWeapon();
            dmg.amount = Damage;
            dmg.type = EDamageTypes.Piercing;

            collision.GetComponent<HealthManager>().Damage(dmg);

            Vector3 pullback = (-collision.transform.right + Vector3.up) * pullBackForce;
            collision.GetComponent<Rigidbody2D>().AddForce(pullback,ForceMode2D.Impulse);
        }
    }
}
