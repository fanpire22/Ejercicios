using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// List of available damage types
/// </summary>
[System.Serializable]
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

[System.Serializable]
public struct SResistances
{
    public EDamageTypes type;
    public float value;
}

public class HealthManager : MonoBehaviour
{
    [SerializeField] List<SResistances> resistances;
    [SerializeField] List<SResistances> weakneses;
    [SerializeField] int MaxHealth;

    private Dictionary<EDamageTypes, float> Resistances;
    private Dictionary<EDamageTypes, float> Weakneses;
    BaseCharacter _ownBaseCharacter;

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
        _ownBaseCharacter = GetComponent<BaseCharacter>();
        Resistances = new Dictionary<EDamageTypes, float>();
        Weakneses = new Dictionary<EDamageTypes, float>();
        foreach (SResistances res in resistances)
        {
            if (!Resistances.ContainsKey(res.type))
            {
                Resistances.Add(res.type, res.value);
            }
        }
        foreach (SResistances wek in weakneses)
        {
            if (!Weakneses.ContainsKey(wek.type))
            {
                Weakneses.Add(wek.type, wek.value);
            }
        }
    }

    public void Damage(WeaponBase weapon)
    {
        foreach (FDamageWeapon dmg in weapon.damages)
        {
            if (Resistances.ContainsKey(dmg.type))
            {
                //Somos resistentes a ese tipo de daño: Reducimos el daño
                CurrentHealth -= Mathf.Max(0, Mathf.FloorToInt(dmg.amount * (1 - Resistances[dmg.type])));
            }
            else if (Weakneses.ContainsKey(dmg.type))
            {

                //Somos débiles a ese tipo de daño: Reducimos el daño. No se puede ser resistente y débil: La resistencia anula la debilidad
                CurrentHealth -= Mathf.Max(0, Mathf.FloorToInt(dmg.amount * (1 + Resistances[dmg.type])));
            }
            else
            {
                CurrentHealth -= dmg.amount;
            }
        }
        if (CurrentHealth < 1)
        {
            //Me he muerto
            OnDeath();
        }
    }
    public void Damage(FDamageWeapon dmg)
    {
        if (Resistances.ContainsKey(dmg.type))
        {
            //Somos resistentes a ese tipo de daño: Reducimos el daño
            CurrentHealth -= Mathf.Max(0, Mathf.FloorToInt(dmg.amount * (1 - Resistances[dmg.type])));
        }
        else if (Weakneses.ContainsKey(dmg.type))
        {

            //Somos débiles a ese tipo de daño: Reducimos el daño. No se puede ser resistente y débil: La resistencia anula la debilidad
            CurrentHealth -= Mathf.Max(0, Mathf.FloorToInt(dmg.amount * (1 + Resistances[dmg.type])));
        }
        else
        {
            CurrentHealth -= dmg.amount;
        }
        if (CurrentHealth < 1)
        {
            //Me he muerto
            OnDeath();
        }
    }

    public void Heal(int Healing)
    {

    }

    protected virtual void OnDeath()
    {
        _ownBaseCharacter.OnDeath();
    }

}
