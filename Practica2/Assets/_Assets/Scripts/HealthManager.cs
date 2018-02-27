using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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

    [SerializeField] Dictionary<EDamageTypes, float> Resistances;
    [SerializeField] Dictionary<EDamageTypes, float> Weakneses;
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

    public void Damage(WeaponBase weapon)
    {
        foreach (FDamageWeapon dmg in weapon.Damages)
        {
            if (Resistances.ContainsKey(dmg.type))
            {
                //Somos resistentes a ese tipo de daño: Reducimos el daño
               CurrentHealth -= Mathf.Max(0,Mathf.FloorToInt(dmg.amount * (1 - Resistances[dmg.type])));
            }
            else if (Weakneses.ContainsKey(dmg.type))
            {

                //Somos débiles a ese tipo de daño: Reducimos el daño. No se puede ser resistente y débil: La resistencia anula la debilidad
                CurrentHealth -= Mathf.Max(0, Mathf.FloorToInt(dmg.amount * (1 + Resistances[dmg.type])));
            }
        }
    }
    
    public void Heal(int Healing)
    {

    }

    protected virtual void OnDeath()
    {
        Destroy(this.gameObject);
    }

}
