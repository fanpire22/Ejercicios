using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// List of available damage types
/// </summary>
public enum EDamageTypes
{
    Fire,
    Water,
    Acid,
    Venom,
    Magic,
    Slashing,
    Piercing,
    Bludgeoning
}

public class HealthManager : MonoBehaviour
{

    [SerializeField] List<EDamageTypes> Resistances;
    [SerializeField] List<EDamageTypes> Weakneses;
    [SerializeField] int MaxHealth;

    public int CurrentHealth { get; private set; }
    public bool isAlive
    {
        get
        {
            return CurrentHealth > 0;
        }
    }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(int Damage)
    {

    }

    public void Heal(int Healing)
    {

    }

    protected virtual void OnDeath()
    {
        Destroy(this.gameObject);
    }

}
