using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETypeOfButton
{
    Simon,
    Weapon
}

/// <summary>
/// Botón activado o por la presencia de Simon o por ser golpeado por un arma
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Lever : MonoBehaviour
{

    [SerializeField] MovingPlat _platform;
    [SerializeField] ETypeOfButton _action = ETypeOfButton.Simon;

    bool bState;
    Animator ani;

    void Awake()
    {
        ani = GetComponent<Animator>();
    }

    /// <summary>
    /// Dependiendo de si es Simon el que entra o bien es un arma
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (_action)
        {
            case ETypeOfButton.Simon:
                if (other.CompareTag("Player"))
                {
                    bState = !bState;
                    ani.SetBool("State", bState);

                    if (bState) _platform.StartMoving();
                    else _platform.StopMoving();
                }
                break;
            case ETypeOfButton.Weapon:
                if (other.CompareTag("Weapon"))
                {
                    bState = !bState;
                    ani.SetBool("State", bState);

                    if (bState) _platform.StartMoving();
                    else _platform.StopMoving();
                }
                break;
            default:
                break;

        }
    }

}
